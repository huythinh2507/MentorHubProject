using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Models;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.DigitalAssetManagement;
using YPP.MH.DigitalAssetManagement.Action;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace YPP.MH.BusinessLogicLayer.Services.PostManagement
{
    public class PostService : IPostService
    {
        private readonly IBaseRepository<Post> _postData;
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        private readonly IConfiguration _config;
        private readonly Dam _dam;

        public PostService(Dam dam, IBaseRepository<Post> postData, IMapper mapper, IDbContextFactory<MHDbContext> dbContextFactory, IConfiguration config)
        {
            _postData = postData;
            _mapper = mapper;
            _dbContextFactory = dbContextFactory;
            _config = config;
            _dam = dam;
        }

        public async Task Comment(int postId, CommentDto commentDto)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.GetPostById(postId);

            if (post.CloseComments == true)
            {
                throw new ArgumentException("Comments are closed for this post.");
            }

            var comment = new Comment
            {
                UserId = commentDto.UserId,
                PostId = postId,
                Content = commentDto.Content,
            };

            post.Comments.Add(comment);
            await context.Comment.AddAsync(comment);

            await context.SaveChangesAsync();
        }

        public async Task<Post> CreatePost(CreatePostView postDto)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            ArgumentNullException.ThrowIfNull(postDto);
            if (postDto.OwnerId <= 0 || postDto.SpaceId <= 0)
            {
                throw new ArgumentException("OwnerId and SpaceGroupId must be positive integers.");
            }
            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized");
            }

            var account = new Account
            {
                TenantId = "DC437619-4038-4FAE-84C0-5A5A5672FE0F",
                ApiKey = "api_key_1",
                SecretKey = "secret_key_1"
            };

            var dam = new Dam(account);

            postDto.Content = await ProcessImagesInContent(postDto.Content, dam);

            var post = _mapper.Map<Post>(postDto);
            await context.Post.AddAsync(post);

            post.SpaceId = postDto.SpaceId;
            await context.SaveChangesAsync();

            var owner = context.User.Where(i => i.Id == postDto.OwnerId);
            ArgumentNullException.ThrowIfNull(owner);
            post.Owner = await owner.FirstAsync();

            return post;
        }

        private async Task<string> ProcessImagesInContent(string htmlContent, Dam dam)
        {
            const string base64Prefix = "data:image/png;base64,";
            int startIndex = 0;
            while ((startIndex = htmlContent.IndexOf(base64Prefix, startIndex)) != -1)
            {
                int endIndex = htmlContent.IndexOf("\"", startIndex + base64Prefix.Length);
                if (endIndex != -1)
                {
                    string before = htmlContent.Substring(0, startIndex);
                    string after = htmlContent[endIndex..];
                    string base64Image = htmlContent[(startIndex + base64Prefix.Length)..endIndex];

                    // Convert base64 to image file
                    var imageFile = ConvertBase64ToFile(base64Image);

                    var uploadParam = new ImageUploadParam { File = imageFile };


                    var url = await dam.UploadAsset(uploadParam);

                    htmlContent = before + url + after;
                }
                else
                {
                    // If we can't find the closing quote, move past this occurrence
                    startIndex += base64Prefix.Length;
                }
            }

            return htmlContent;
        }

        private IFormFile ConvertBase64ToFile(string base64Image)
        {
            byte[] bytes = Convert.FromBase64String(base64Image);
            MemoryStream stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, "image", "image.png")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/png"
            };
        }

        public async Task<PagedResult<PostDto>> GetHomePagePostsAsync(int userId, int page, int pageSize, string sortBy, bool descending)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var spaceIds = await context.SpaceMember
                .Where(sm => sm.MemberId == userId)
                .Select(sm => sm.SpaceId)
                .ToListAsync();

            var query = context.Post
                .Where(p => p.IsPublished == true && spaceIds.Contains(p.SpaceId))
                .Include(p => p.Owner)
                .Include(p => p.Space)
                .Include(p => p.Comments)
                .ThenInclude(l => l.User);

            var posts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = (await Task.WhenAll(posts.Select(GetPostDetailsAsync)))
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

            return new PagedResult<PostDto>
            {
                Items = [.. items],
                Page = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };
        }

        public async Task<PostDto> GetPost(int id)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.Post
                .Include(p => p.Owner)
                .Include(p => p.Comments)
                .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            ArgumentNullException.ThrowIfNull(post);

            return await GetPostDetailsAsync(post);
        }

        public async Task<PagedResult<PostDto>> GetTrendingPostsAsync(int page, int pageSize)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var query = context.Post
                .Include(p => p.Owner)
                .Include(p => p.Space)
                .Include(p => p.Comments)
                .ThenInclude(l => l.User)
                .OrderByDescending(p => p.CreatedAt);

            var posts = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = (await Task.WhenAll(posts.Select(GetPostDetailsAsync)))
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

            return new PagedResult<PostDto>
            {
                Items = [.. items],
                Page = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };
        }

        private async Task<PostDto> GetPostDetailsAsync(Post post)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            ArgumentNullException.ThrowIfNull(post);

            var userRoleInSpace = await context.WorkspaceMember
                .Where(wm => wm.UserId == post.OwnerId && wm.WorkspaceId == post.SpaceId)
                .Select(wm => wm.Role)
                .FirstOrDefaultAsync();

            var postId = post.Id;
            var likedByUsers = await context.Like
                .Where(l => l.PostId == postId)
                .Select(l => l.User)
                .Where(user => user != null)
                .Select(user => new User
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    ProfileImg = user.ProfileImg
                })
                .ToListAsync();

            var commentCount = context.Comment
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .OrderByDescending(c => c.CommentedAt).CountAsync().Result;

            return new PostDto
            {
                Id = post.Id,
                OwnerId = post.OwnerId,
                OwnerName = post.Owner?.FullName,
                SpaceName = post.Space.Name,
                Title = post.Title,
                Content = post.Content,
                FileId = post.FileId,
                CoverImg = post.CoverImg,
                IsPublished = post.IsPublished,
                TimeSchedule = post.TimeSchedule,
                Link = post.Link,
                CreatedAt = post.CreatedAt,
                Comments = [],
                CommentsCount = commentCount,
                OwnerProfileImg = post.Owner?.ProfileImg,
                SpaceRole = userRoleInSpace,
                LikedByUsers = likedByUsers,
                SpaceId = post.Space.Id
            };
        }

        public async Task Like(int postId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.GetPostById(postId);

            

            var like = new Like
            {
                UserId = userId,
                PostId = postId
            };

            post.Likes.Add(like);
            await context.Like.AddAsync(like);

            await context.SaveChangesAsync();
        }

        public async Task<bool> HasUserLikedPost(int postId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Like.AnyAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task Dislike(int postId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var like = await context.Like
                                    .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);

            if (like != null)
            {
                context.Like.Remove(like);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Comment> GetComment(int postId, int commentId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var comment =  await context.Comment.FirstOrDefaultAsync(c => c.Id == commentId && c.PostId == postId);
            
            ArgumentNullException.ThrowIfNull(comment);
            return comment;
        }

        public async Task<bool> HasUserLikedComment(int commentId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            return await context.Like.AnyAsync(l => l.CommentId == commentId && l.UserId == userId);
        }

        public async Task LikeComment(int postId, int commentId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var comment = await context.Comment.FindAsync(commentId);

            var like = new Like
            {
                PostId = postId,
                UserId = userId,
                CommentId = commentId
            };

            ArgumentNullException.ThrowIfNull(comment);
            comment.Likes.Add(like);
            await context.Like.AddAsync(like);

            await context.SaveChangesAsync();
        }

        public async Task DislikeComment(int commentId, int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var like = await context.Like
                                    .FirstOrDefaultAsync(l => l.PostId == commentId && l.UserId == userId);

            if (like != null)
            {
                context.Like.Remove(like);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Comment> ReplyToComment(int commentId, int userId, string content)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            var parentComment = await context.Comment.FindAsync(commentId);
            if (parentComment == null)
            {
                throw new ArgumentException("Parent comment not found.");
            }

            var reply = new Comment
            {
                UserId = userId,
                PostId = parentComment.PostId,
                ParentCommentId = parentComment.Id,
                Content = content
            };

            context.Comment.Add(reply);
            await context.SaveChangesAsync();

            return reply;
        }

        public async Task DeletePost(int postId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var post = await context.Post.FindAsync(postId) ?? throw new ArgumentException("Post not found.");
            
            await RemoveLikesForPost(postId);

            await RemoveCommentsForPost(postId);
            
            context.Post.Remove(post);
            await context.SaveChangesAsync();
        }

        public async Task RemoveLikesForPost(int postId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var likes = await context.Like.Where(l => l.PostId == postId).ToListAsync();

            context.Like.RemoveRange(likes);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCommentsForPost(int postId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var comments = await context.Comment.Where(c => c.PostId == postId).ToListAsync();

            context.Comment.RemoveRange(comments);
            await context.SaveChangesAsync();
        }

        public async Task DeleteComment(int cmtId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var comment = await context.Comment.FindAsync(cmtId) ?? throw new ArgumentException("Post not found.");

            await RemoveLikesForComment(cmtId);

            context.Comment.Remove(comment);
            await context.SaveChangesAsync();
        }

        private async Task RemoveLikesForComment(int cmtId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var likes = await context.Like.Where(l => l.CommentId == cmtId).ToListAsync();

            context.Like.RemoveRange(likes);
            await context.SaveChangesAsync();
        }

        public async Task<PagedResult<CommentDto>> GetPostCommentsAsync(int postId, int page, int pageSize)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var query = context.Comment
                .Where(c => c.PostId == postId && c.ParentComment == null)
                .Include(c => c.User)
                .Include(c => c.Replies)
                .ThenInclude(r => r.User)
                .OrderByDescending(c => c.CommentedAt);

            var totalCount = await query.CountAsync();

            var comments = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    UserId = c.UserId.Value,
                    UserName = c.User.FullName,
                    UserProfileImg = c.User.ProfileImg,
                    Content = c.Content,
                    CommentedAt = c.CommentedAt,
                    LikeCount = c.Likes.Count,
                    Replies = c.Replies.Select(r => new CommentDto
                    {
                        Id = r.Id,
                        UserId = r.UserId.Value,
                        UserName = r.User.FullName,
                        UserProfileImg = r.User.ProfileImg,
                        Content = r.Content,
                        CommentedAt = r.CommentedAt,
                        LikeCount = r.Likes.Count
                    }).ToList()
                })
                .ToListAsync();

            return new PagedResult<CommentDto>
            {
                Items = comments,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }
        public async Task<PostDto> EditPost(int ownerId, int spaceId, int postId, EditPostDto editPostDto)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.Post.FindAsync(postId) ?? throw new ArgumentException("Post not found.");
            post.Content = editPostDto.Content;
           
            context.Update(post);
            post.SpaceId = spaceId;

            var owner = context.User.Where(i => i.Id == ownerId);
            ArgumentNullException.ThrowIfNull(owner);
            post.Owner = await owner.FirstAsync();

            var space = context.Space.Where(i => i.Id == spaceId);
            ArgumentNullException.ThrowIfNull(space);
            post.Space = await space.FirstAsync();

            await context.SaveChangesAsync();

            return await GetPostDetailsAsync(post);
        }

        public async Task<PostDto> DuplicatePost(int ownerId, int postId, int newSpaceId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.Post.FindAsync(postId) ?? throw new ArgumentException("Post not found.");

            var owner = context.User.Where(i => i.Id == ownerId).FirstAsync().Result;
            ArgumentNullException.ThrowIfNull(owner);

            var space = context.Space.Where(i => i.Id == newSpaceId).FirstAsync().Result;
            ArgumentNullException.ThrowIfNull(space);

            var newPost = new Post()
            {
                SpaceId = newSpaceId,
                OwnerId = post.OwnerId,
                Comments = [],
                Owner = null,
                Space = new Space(),
                Title = post.Title,
                Content = post.Content,
                IsPublished = true
            };

            await context.Post.AddAsync(newPost);

            newPost.SpaceId = newSpaceId;

            await context.SaveChangesAsync();
            newPost.Owner = owner;
            newPost.Space = space;
            return await GetPostDetailsAsync(newPost);
        }

        public async Task UpdatePostSettings(int postId, PostSettingsDto settings)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var post = await context.Post.FindAsync(postId) ?? throw new ArgumentException("Post not found.");
            post.HideLikes = settings.HideLikes;
            post.HideComments = settings.HideComments;
            post.CloseComments = settings.CloseComments;

            await context.SaveChangesAsync();
        }
    }
}
