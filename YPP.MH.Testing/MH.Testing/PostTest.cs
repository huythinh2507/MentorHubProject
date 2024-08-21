using AutoMapper;
using YPP.MH.DigitalAssetManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Models;
using YPP.MH.BusinessLogicLayer.Services.PostManagement;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.PresentationLayer.Controllers;
using YPP.MH.PresentationLayer.Mappings;

namespace YPP.MH.Testing
{
    public class PostTest
    {
        private readonly PostController _postController;
        private readonly PostService _postService;
        private readonly IUserService _fakeUserService;
        private readonly IBaseRepository<Post> _postData;
        private readonly IBaseRepository<User> _userData;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        private readonly Dam dam;
        public PostTest()
        {
            // Configure in-memory database
            var dbContextOptions = new DbContextOptionsBuilder<MHDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            _dbContextFactory = new DbContextFactory(dbContextOptions);

            // Configuration for testing
            _config = InitConfiguration();

            // Setup repositories
            _postData = new BaseRepository<Post>(_dbContextFactory);
            _userData = new BaseRepository<User>(_dbContextFactory);

            // Setup AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            });
            _mapper = mapperConfig.CreateMapper();

            // Setup services
            _postService = new PostService(dam, _postData, _mapper, _dbContextFactory, _config);
            _fakeUserService = new UserService(_dbContextFactory, _userData, _mapper, _config);
           // _postController = new PostController(_postService, _fakeUserService);
        }

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            return config;
        }

        [Fact]
        public async Task Test_CreatePost()
        {
            // Arrange
            var post = new CreatePostView
            {
                OwnerId = 1,
                SpaceId = 1,
                IsPublished = true,
                Title = "Test Post",
                Content = "This is a test post."
            };

            // Act
            var result = await _postController.CreatePost(post);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdPost = Assert.IsType<Post>(okResult.Value);
            Assert.Equal(post.Title, createdPost.Title);
        }

        [Fact]
        public async Task Test_GetAPost()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Test Post" };

            using var context = await _dbContextFactory.CreateDbContextAsync();
            await context.Post.AddAsync(post);
            await context.SaveChangesAsync();

            // Act
            var result = await _postController.GetPost(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var fetchedPost = Assert.IsType<PostDto>(okResult.Value);
            Assert.Equal(post.Title, fetchedPost.Title);
        }

        [Fact]
        public async Task Test_GetUserHomepagePosts()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Post 1", SpaceId = 1, IsPublished = true };
            using var context = await _dbContextFactory.CreateDbContextAsync();
            await context.Post.AddAsync(post);
            await context.SaveChangesAsync();

            // Act
            var result = await _postController.GetHomePagePosts(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var pagedResult = Assert.IsType<PagedResult<PostDto>>(okResult.Value);
            Assert.Single(pagedResult.Items);
        }

        [Fact]
        public async Task Test_PostAComment()
        {
            // Arrange
            var post = new Post { Id = 1, Title = "Test Post" };
            using var context = await _dbContextFactory.CreateDbContextAsync();
            await context.Post.AddAsync(post);
            await context.SaveChangesAsync();

            var commentDto = new CommentDto { UserId = 1, Content = "Test comment" };

            // Act
            var result = await _postController.AddCommentToPost(post.Id, commentDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            var updatedPost = await context.Post.FindAsync(post.Id);
            Assert.Contains(updatedPost.Comments, c => c.Content == "Test comment");
        }

        // Custom implementation of IDbContextFactory
        private class DbContextFactory : IDbContextFactory<MHDbContext>
        {
            private readonly DbContextOptions<MHDbContext> _options;

            public DbContextFactory(DbContextOptions<MHDbContext> options)
            {
                _options = options;
            }

            public MHDbContext CreateDbContext()
            {
                return new MHDbContext(_options);
            }
        }
    }
}
