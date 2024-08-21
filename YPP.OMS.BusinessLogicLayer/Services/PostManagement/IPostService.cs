using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Models;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.Services.PostManagement
{
    public interface IPostService
    {
        Task<PagedResult<PostDto>> GetHomePagePostsAsync(
            int userId,
            int page,
            int pageSize,
            string sortBy,
            bool descending);
        Task<Post> CreatePost(CreatePostView postDto);
        Task<PostDto> GetPost(int id);
        Task Comment(int postId, CommentDto commentDto);
    }
}
