using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.PresentationLayer.ViewModels;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public partial class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public AuthenticationController(IUserService userService, IConfiguration configuration, IEmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserViewModel userViewModel)
        {
            try
            {
                var user = await _userService.Validate(userViewModel.Email!, userViewModel.Password!);
                if (user == null)
                {
                    return BadRequest("Invalid username or password.");
                }

                if (user.EmailConfirmed == false)
                {
                    return BadRequest("Please confirm your registration email.");
                }

                string token = CreateToken(user);

                // Check if the user requested "Remember Me"
                if (userViewModel.RememberMe)
                {
                    // Generate a unique "Remember Me" token
                    var rememberMeToken = Guid.NewGuid().ToString();

                    // Store the token associated with the user's account
                    await _userService.StoreRememberMeToken(user.Id, rememberMeToken);

                    // Set the "Remember Me" cookie
                    SetRememberMeCookie(rememberMeToken);
                }

                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("auto-login")]
        public async Task<ActionResult> AutoLogin()
        {
            // Check if the "Remember Me" cookie is present
            if (Request.Cookies.TryGetValue("RememberMe", out var rememberMeToken))
            {
                // Retrieve the user by the "Remember Me" token
                var user = await _userService.GetUserByRememberMeToken(rememberMeToken);
                if (user != null)
                {
                    // Generate a new token and update the "Remember Me" token
                    var newToken = _userService.CreateToken(user, _configuration);
                    await _userService.StoreRememberMeToken(user.Id, rememberMeToken);
                    return Ok(new { Token = newToken });
                }
            }

            return Unauthorized("Unable to auto-login. Please login again.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private void SetRememberMeCookie(string rememberMeToken)
        {
            // Set the "Remember Me" cookie with the generated token
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(30) // Cookie expires in 30 days
            };
            Response.Cookies.Append("RememberMe", rememberMeToken, cookieOptions);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserViewModel user)
        {
            try
            {
                
                var loginCode = new Random().Next(100000, 999999).ToString();

                if (!_userService.ValidateFullName(user.FullName))
                {
                    return BadRequest("Full Name is not valid. It should contain only letters and spaces.");
                }

                if (!_userService.ValidateEmail(user.Email))
                {
                    return BadRequest("Email is not valid.");
                }

                if (!_userService.ValidatePassword(user.Password))
                {
                    return BadRequest("Password must be at least 8 characters long.");
                }

                var existingUser = await _userService.GetUserByEmailAsync(user.Email);
                if (existingUser != null)
                {
                    return BadRequest("An account with this email already exists.");
                }

                // Create a new user
                var newUser = new User
                {
                    FullName = user.FullName,
                    Email = user.Email!,
                    Password = user.Password!,
                    EmailConfirmed = false,
                    ConfirmationToken = loginCode,
                    Role = RoleType.Mentee.ToString(),
                    JoinedOn = DateTime.Now,
                };

                await _userService.CreateUser(newUser);

                await _emailService.SendConfirmationEmailAsync(newUser.Email, loginCode);

                return Ok("Please check your email to verify your account.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return Ok("A password reset link has been sent.");
            }

            var token = await _userService.GeneratePasswordResetTokenAsync(user.Id);
            var resetLink = $"{_configuration["AppSettings:FrontendBaseUrl"]}/reset-password?token={token}&userId={user.Id}";

            await _emailService.SendPasswordResetEmailAsync(email, resetLink);

            return Ok("A password reset link has been sent to your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            var result = await _userService.ResetPasswordAsync(model.UserId, model.Token, model.NewPassword);
            if (result)
            {
                return Ok("Your password has been reset successfully.");
            }
            else
            {
                return BadRequest("Invalid or expired password reset token.");
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(int userId, string token)
        {
            var user = await _userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.EmailConfirmed == true)
            {
                return BadRequest("Email already confirmed");
            }

            if (user.ConfirmationToken != token)
            {
                return BadRequest("Invalid confirmation token");
            }

            if (user.ConfirmationToken == token)
            {
                user.EmailConfirmed = true;
                user.ConfirmationToken = null;
                _userService.UpdateUser(user);
            }

            return Ok(200);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string CreateToken(User user)
        {
            ArgumentNullException.ThrowIfNull(user.FullName);
            ArgumentNullException.ThrowIfNull(user.Email);

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("Id", user.Id.ToString())
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Check if the "Remember Me" cookie is present
            if (Request.Cookies.TryGetValue("RememberMe", out var rememberMeToken))
            {
                // Retrieve the user by the "Remember Me" token
                var user = await _userService.GetUserByRememberMeToken(rememberMeToken);
                if (user != null)
                {
                    // Clear the "Remember Me" token
                    await _userService.ClearRememberMeToken(user.Id);

                    // Clear the "Remember Me" cookie
                    Response.Cookies.Delete("RememberMe");
                }
            }

            return Ok();
        }




    }
}
