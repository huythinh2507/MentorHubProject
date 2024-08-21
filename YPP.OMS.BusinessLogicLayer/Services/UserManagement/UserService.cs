using AutoMapper;
using GraphQL.Types.Relay.DataObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using YPP.MH.BusinessLogicLayer.DTOs;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;

namespace YPP.MH.BusinessLogicLayer.Services.UserManagement
{
    public partial class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userData;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        public UserService(IDbContextFactory<MHDbContext> context, IBaseRepository<User> userData, IMapper mapper, IConfiguration configuration)
        {
            _userData = userData;
            _mapper = mapper;
            _config = configuration;
            _dbContextFactory = context;
        }

        public async Task<User> Validate(string email, string password)
        {
            await ValidateUserInfoInternal(email, password)!;

            await using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await context.User.FirstOrDefaultAsync(u => u.Email == email);

            ArgumentNullException.ThrowIfNull(user);
            return user;
        }

        private async Task<bool> ValidateUserInfoInternal(string email, string? password)
        {
            var user = await GetUserByEmailAsync(email);

            ArgumentNullException.ThrowIfNull(user);
            return BCrypt.Net.BCrypt.Verify(password, user.Password) ? true : throw new Exception("Invalid password!");
        }

        public async Task<bool> ChangePassword(int id, string currentPassword, string newPassword)
        {
            var userInfo = await _userData.Get().FirstOrDefaultAsync(x => x.Id.Equals(id));
            var checkingPassword = BCrypt.Net.BCrypt.Verify(currentPassword, userInfo!.Password);
            var hasChangePassword = ChangeUserPasswordInternal(userInfo, checkingPassword, newPassword);
            return hasChangePassword;
        }

        private bool ChangeUserPasswordInternal(User userInfo, bool checkingPassword, string newPassword)
        {

            var hashPassword = (checkingPassword) ? BCrypt.Net.BCrypt.HashPassword(newPassword) : throw new Exception("Invalid password");
            userInfo.Password = hashPassword;
            _userData.Update(userInfo);
            return BCrypt.Net.BCrypt.Verify(newPassword, userInfo.Password);
        }

        public string CreateToken(User user, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(user.FullName);
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("Id", user.Id.ToString()),
            ];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("AppSettings:Token").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(24),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public string GetUserImage(int id)
        {
            return _userData.Get(ui => ui.Id.Equals(id)).FirstOrDefault()!.ProfileImg!;
        }

        public async Task<User> CreateUser(User userInfo)
        {
            ArgumentNullException.ThrowIfNull(userInfo);

            if (_mapper == null)
            {
                throw new InvalidOperationException("Mapper is not initialized");
            }

            var user = _mapper.Map<User>(userInfo);

            if (string.IsNullOrEmpty(userInfo.Password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(userInfo));
            }

            ArgumentNullException.ThrowIfNull(_userData);
            user.Password = BCrypt.Net.BCrypt.HashPassword(userInfo.Password);
            await SaveUser(user);
            return user;
        }

        public async Task SaveUser(User user)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();
            context.User.Add(user);

            await context.SaveChangesAsync();
        }

        public async Task StoreRememberMeToken(int userId, string rememberMeToken)
        {
            var user = await _userData
                .Get(x => x.Id.Equals(userId)).FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user);
            user.RememberMeToken = rememberMeToken;
            await _userData.Update(user);
        }

        public async Task<User> GetUserByRememberMeToken(string rememberMeToken)
        {
            var user = await _userData
               .Get(x => (x.RememberMeToken ?? string.Empty).Equals(rememberMeToken)).FirstOrDefaultAsync();

            ArgumentNullException.ThrowIfNull(user);
            return user;
        }

        public async Task ClearRememberMeToken(int id)
        {
            var user = await _userData
               .Get(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            ArgumentNullException.ThrowIfNull(user);
            user.RememberMeToken = null;
            await _userData.Update(user);
        }

        public void UpdateUser(User user)
        {
            using var context = _dbContextFactory.CreateDbContext();
            context.User.Update(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await context.User.FirstOrDefaultAsync(u => u.Email == email);
            return user; // This can be null if no user is found
        }

        public async Task<User> GetUserById(int userId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await context.User.FirstOrDefaultAsync(u => u.Id == userId);
            ArgumentNullException.ThrowIfNull(user);
            return user;
        }

        public async Task<List<User>> GetMentors()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var mentors = await context.User
                .Where(u => u.Role == RoleType.Mentor.ToString())
                .ToListAsync();
            return mentors;
        }

        public async Task<List<Article>> GetArticles()
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var articles = await context.Article.ToListAsync();
            return articles;
        }
        public async Task<List<UserWithSpacesDto>> GetWorkspaceMembersByOwnerIdAsync(int ownerId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();

            var workspaceIds = await context.WorkSpace
                .Where(w => w.OwnerId == ownerId)
                .Select(w => w.Id)
                .ToListAsync();

            var membersWithSpaces = await context.SpaceMember
                .Where(sm => workspaceIds.Contains(sm.SpaceId))
                .Join(context.User, sm => sm.MemberId, u => u.Id, (sm, u) => new { sm.SpaceId, User = u })
                .Join(context.Space, smu => smu.SpaceId, s => s.Id, (smu, s) => new { smu.User, SpaceName = s.Name })
                .GroupBy(us => us.User)
                .Select(g => new UserWithSpacesDto
                {
                    Id = g.Key.Id,
                    FullName = g.Key.FullName,
                    Email = g.Key.Email,
                    Role = g.Key.Role,
                    ProfileImg = g.Key.ProfileImg,
                    JoinedOn = g.Key.JoinedOn,
                    JobTitle = g.Key.JobTitle,
                    SpaceNames = g.Select(us => us.SpaceName).ToList()
                })
                .ToListAsync();

            return membersWithSpaces;
        }
        public async Task<UserProfileDto> GetProfile(int userId)
        {
            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var user = await context.User
                .FirstOrDefaultAsync(u => u.Id == userId);
            ArgumentNullException.ThrowIfNull(user);

            var posts = await context.Post
                .Where(p => p.OwnerId == userId)
                .ToListAsync();

            var likedPosts = await context.Like
                .Where(l => l.UserId == userId)
                .Include(l => l.Post)
                .Select(l => l.Post)
                .ToListAsync();

            var userSpaces = await context.SpaceMember
                .Where(sm => sm.MemberId == userId)
                .Include(sm => sm.Space)
                .Select(sm => new SpaceInfo
                {
                    Name = sm.Space.Name,
                    MemberCount = sm.Space.Members.Count
                })
                .ToListAsync();

            return new UserProfileDto
            {
                User = user,
                Posts = posts,
                LikedPosts = likedPosts,
                SpacesCount = userSpaces.Count,
                Spaces = userSpaces
            };
        }

        public async Task InviteUserAsync(InviteUserRequest request)
        {
            var role = Enum.TryParse<RoleType>(request.Role, out var parsedRole) ? parsedRole : RoleType.Mentee;



            await using var context = await _dbContextFactory.CreateDbContextAsync();

            var user = await context.User
            .Where(u => u.Email == request.Email)
            .Select(u => new { u.Id })
            .FirstOrDefaultAsync();

            if (user != null)
            {
                var existingMembership = await context.SpaceMember
                    .AnyAsync(m => m.MemberId == user.Id);

                if (existingMembership)
                {
                    throw new InvalidOperationException("User is already a member of this space.");
                }
            }

            var existingInvitation = await context.Invitation
                .FirstOrDefaultAsync(i => i.UserEmail == request.Email && i.SpaceId == request.SpaceId);

            if (existingInvitation != null)
            {
                throw new InvalidOperationException("User is already invited to this space.");
            }

            var invitation = new Invitation
            {
                UserName = request.Name,
                SpaceId = request.SpaceId,
                UserEmail = request.Email,
                TenantId = request.TenantId,
                Role = role.ToString(),
            };

            context.Invitation.Add(invitation);
            await context.SaveChangesAsync();
        }

        [GeneratedRegex(@"^[a-zA-Z\s'-]+$")]
        private static partial Regex FullNameRegex();

        [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")]
        private static partial Regex EmailRegax();

        public bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return false;

            if (fullName.Length < 2 || fullName.Length > 100)
                return false;

            return FullNameRegex().IsMatch(fullName);
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            if (email.Length > 254)
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8 || password.Length > 128)
                return false;
            return true;
        }

        public async Task<List<UserWithSpacesDto>> FilterMembers(List<UserWithSpacesDto> members, string? name, string? spaceName)
        {
            if (!string.IsNullOrEmpty(name))
            {
                members = members.Where(m => m.FullName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(spaceName))
            {
                members = members.Where(m => m.SpaceNames.Contains(spaceName, StringComparer.OrdinalIgnoreCase)).ToList();
            }

            return members;
        }

        public async Task<Article> GetArticle(int articleId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var article = await context.Article.FirstOrDefaultAsync(a => a.Id == articleId);

            ArgumentNullException.ThrowIfNull(article);
            return article;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(int userId)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await GetUserById(userId) ?? throw new ArgumentException("User not found");
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.PasswordResetTokenExpiration = DateTime.UtcNow.AddHours(24); // Token valid for 24 hours

            context.User.Update(user);
            return token;
        }

        public async Task<bool> ResetPasswordAsync(int userId, string token, string newPassword)
        {
            using var context = await _dbContextFactory.CreateDbContextAsync();
            var user = await GetUserById(userId);
            if (user == null || user.PasswordResetToken != token || user.PasswordResetTokenExpiration < DateTime.UtcNow)
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.PasswordResetToken = null;
            user.PasswordResetTokenExpiration = null;

            context.User.Update(user);
            return true;
        }
    }

    public class UserWithSpacesDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<string> SpaceNames { get; set; } = [];
        public string? ProfileImg { get; internal set; }
        public DateTime? JoinedOn { get; internal set; }
        public string? JobTitle { get; internal set; }
    }
}
