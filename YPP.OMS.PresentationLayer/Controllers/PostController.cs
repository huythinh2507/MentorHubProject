using AutoMapper;
using DigitalAssetManagement;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Models;
using YPP.MH.BusinessLogicLayer.Services.PostManagement;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.PresentationLayer.ViewModels;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/posts")]
    [ApiController]
        public class PostController : ControllerBase
        {
            private readonly PostService _postService;
            private readonly IUserService _userService;
            private readonly IMapper _mapper;

        public PostController(PostService postService, IUserService userService, IMapper mapper)
            {
                _postService = postService;
                _userService = userService;
                _mapper = mapper;
            }

        [HttpGet("home/{userId}")]
        [Authorize]
        public async Task<ActionResult<PagedResult<PostDto>>> GetHomePagePosts(
            int userId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "CreatedAt",
            [FromQuery] bool descending = true)
        {
            try
            { 
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var posts = await _postService.GetHomePagePostsAsync(
                    userId,
                    page,
                    pageSize,
                    sortBy,
                    descending);
                return Ok(posts);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost("create-post")]
        [RequestSizeLimit(100_000_000)] 
        [RequestFormLimits(MultipartBodyLengthLimit = 100_000_000)]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostView createPostDto)
        {
            try
            {
                var post = await _postService.CreatePost(createPostDto);

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await _postService.GetPost(postId);
            if (post == null)
            {
                return BadRequest("Post not found.");
            }
            return Ok(post);
        }

        [HttpPost("{postId}/comment")]
        public async Task<IActionResult> AddCommentToPost(int postId, [FromBody] CommentDto comment)
        {
            if (comment == null)
            {
                return BadRequest("Invalid comment data.");
            }

            var post = await _postService.GetPost(postId);

            if (post == null)
            {
                return BadRequest("Post not found.");
            }

            await _postService.Comment(postId, comment);

            return Ok(200);
        }

        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikeAPost(int postId, int userId)
        {
            var post = await _postService.GetPost(postId);

            if (post == null)
            {
                return BadRequest("Post not found.");
            }

            var hasLiked = await _postService.HasUserLikedPost(postId, userId);
            if (hasLiked)
            {
                await _postService.Dislike(postId, userId);
                return Ok(200);
            }

            // Otherwise, like the post
            await _postService.Like(postId, userId);
            
            return Ok(200);
        }

        [HttpPost("{postId}/comments/{commentId}/like")]
        public async Task<IActionResult> LikeAComment(int postId, int commentId, int userId)
        {
            var comment = await _postService.GetComment(postId, commentId);

            if (comment == null)
            {
                return BadRequest("Comment not found.");
            }

            var hasLiked = await _postService.HasUserLikedComment(commentId, userId);
            if (hasLiked)
            {
                await _postService.DislikeComment(postId, userId);
                return Ok(200);
            }

            await _postService.LikeComment(postId, commentId, userId);

            return Ok(200);
        }

        [HttpPost("{postId}/comment/{commentId}/reply")]
        public async Task<IActionResult> ReplyToComment(int postId, int commentId, [FromBody] ReplyDto replyDto)
        {
            if (replyDto == null || string.IsNullOrEmpty(replyDto.Content))
            {
                return BadRequest("Invalid reply data.");
            }

            try
            {
                var reply = await _postService.ReplyToComment(commentId, replyDto.UserId, replyDto.Content);

                return Ok(reply);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpGet("trending")]
        public async Task<IActionResult> GetTrendingPosts()
        {
            var post = await _postService.GetTrendingPostsAsync(1, 5);

            if (post == null)
            {
                return BadRequest("Post not found.");
            }

            return Ok(post);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            try
            {
                await _postService.DeletePost(postId);
                return NoContent(); // No content is returned after a successful deletion
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpDelete("comments/{cmtId}")]
        public async Task<IActionResult> DeleteComment(int cmtId)
        {
            try
            {
                await _postService.DeleteComment(cmtId);
                return Ok(204); // No content is returned after a successful deletion
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpGet("{postId}/comments")]
        public async Task<ActionResult<PagedResult<CommentDto>>> GetPostComments(int postId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                var post = await _postService.GetPost(postId);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                var comments = await _postService.GetPostCommentsAsync(postId, page, pageSize);
                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        [HttpPatch("{postId}")]
        public async Task<IActionResult> EditPost(int ownerId, int spaceId, int postId, [FromBody] EditPostDto editPostDto)
        {
            try
            {
                var updatedPost = await _postService.EditPost(ownerId, spaceId, postId, editPostDto);
                return Ok(updatedPost);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{postId}/duplicate")]
        public async Task<IActionResult> DuplicatePost(int postId, int ownerId, [FromBody] DuplicatePostDto duplicatePostDto)
        {
            try
            {
                var duplicatedPost = await _postService.DuplicatePost(ownerId, postId, duplicatePostDto.NewSpaceId);
                return Ok(duplicatedPost);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{postId}/settings")]
        public async Task<IActionResult> UpdatePostSettings(int postId, [FromBody] PostSettingsDto settings)
        {
            try
            {
                await _postService.UpdatePostSettings(postId, settings);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
