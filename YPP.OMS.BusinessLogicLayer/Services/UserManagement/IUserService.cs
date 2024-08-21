using Microsoft.Extensions.Configuration;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.BusinessLogicLayer.Services.UserManagement
{
    public interface IUserService
    {
        Task<bool> ChangePassword(int id, string currentPassword, string newPassword);
        Task ClearRememberMeToken(int id);
        string CreateToken(User user, IConfiguration configuration);
        Task<User> CreateUser(User userInfo);
        Task<User> GetUserByRememberMeToken(string rememberMeToken);
        Task<User?> GetUserByEmailAsync(string email);
        string GetUserImage(int id);
        Task StoreRememberMeToken(int userId, string rememberMeToken);
        Task<User> Validate(string email, string password);
        Task<User> GetUserById(int id);
        void UpdateUser(User user);
        Task<List<User>> GetMentors();
        Task<List<Article>> GetArticles();
        bool ValidateFullName(string fullName);
        bool ValidateEmail(string email);
        bool ValidatePassword(string password);
        Task<Article> GetArticle(int articleId);
        Task<string> GeneratePasswordResetTokenAsync(int userId);
        Task<bool> ResetPasswordAsync(int userId, string token, string newPassword);
    }
}
