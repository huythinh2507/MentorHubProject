using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

    }
}
