using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/member")]
    [ApiController]
    public class MemberPageController : ControllerBase
    {
        private readonly UserService _userService;

        public MemberPageController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userId}/members")]
        public async Task<IActionResult> GetMembers(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var members = await _userService.GetWorkspaceMembersByOwnerIdAsync(userId);

            if (members == null || members.Count == 0)
            {
                return NotFound("No user found.");
            }

            return Ok(members);
        }

        [HttpGet("{userId}/members/name={name}/space={spaceName}")]
        public async Task<IActionResult> GetMembers(int userId, [FromQuery] string? name, [FromQuery] string? spaceName)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var members = await _userService.GetWorkspaceMembersByOwnerIdAsync(userId);

            if (members == null || members.Count == 0)
            {
                return NotFound("No user found.");
            }

            var result = await _userService.FilterMembers(members, name, spaceName);

            return Ok(result);
        }

        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetProfile(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest("Invalid user ID.");
            }

            var user = await _userService.GetProfile(userId);

            return Ok(user);
        }

        [HttpPost("invite")]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserRequest request)
        {
            if (request.SpaceId <= 0 || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Group ID and user email are required.");
            }

            try
            {
                if (!_userService.ValidateFullName(request.Name))
                {
                    return BadRequest("Invalid full name.");
                }

                if (!_userService.ValidateEmail(request.Email))
                {
                    return BadRequest("Invalid email address.");
                }

                await _userService.InviteUserAsync(request);
                return Ok("User invited successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. " + ex.Message);
            }
        }
    }
}
