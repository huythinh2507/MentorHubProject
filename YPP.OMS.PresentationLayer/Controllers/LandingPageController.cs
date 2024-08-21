using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/landingpage")]
    [ApiController ]
    public class LandingPageController : ControllerBase
    {
        private readonly IUserService _userService;
        public LandingPageController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _userService.GetArticles();

            return Ok(articles);
        }

        [HttpGet("articles/{articleId}")]
        public async Task<IActionResult> GetArticle(int articleId)
        {
            var article = await _userService.GetArticle(articleId);

            return Ok(article);
        }


        [HttpGet("mentors")]
        public async Task<IActionResult> GetMentors()
        {
            var mentors = await _userService.GetMentors();

            return Ok(mentors);
        }
    }
}
