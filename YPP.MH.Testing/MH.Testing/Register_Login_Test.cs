using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.PresentationLayer.Controllers;
using YPP.MH.PresentationLayer.Mappings;
using YPP.MH.PresentationLayer.ViewModels;

namespace MH.Testing
{
    public class Register_Login_Test
    {
        private readonly AuthenticationController _fakeAuthenticationController;
        private readonly IUserService _fakeUserService = A.Fake<IUserService>();
        private readonly IConfiguration _config = InitConfiguration();
        private readonly IEmailService _fakeEmailService = A.Fake<IEmailService>();
        private readonly IBaseRepository<User> _userData;
        private readonly IMapper _mapper;
        private readonly IDbContextFactory<MHDbContext> _dbContextFactory;
        public Register_Login_Test()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperConfig>(); // Assuming you have a MappingProfile class
            });
            _mapper = config.CreateMapper();
            _fakeAuthenticationController = new AuthenticationController(_fakeUserService, _config, _fakeEmailService);

            var dbContextOptions = new DbContextOptionsBuilder<MHDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            _dbContextFactory = new DbContextFactory(dbContextOptions);
        }

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

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();
            return config;
        }

        [Fact]
        public async Task Register_SendsRealEmail_ToTestEmailAddress()
        {
            // Arrange
            var configuration = InitConfiguration();
            var userService = new UserService(_dbContextFactory, _userData, _mapper, _config);
            var emailService = new EmailService(configuration);
            var authController = new AuthenticationController(userService, configuration, emailService);

            var testEmail = "huythinh2507@gmail.com"; // Replace with your actual email
            var registerViewModel = new UserViewModel
            {
                FullName = "Test User",
                Password = "testPassword123",
                Email = testEmail,
                UserName = "testuser"
            };

            // Act
            var result = await authController.Register(registerViewModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_ConfirmationCode()
        {
            var configuration = InitConfiguration();
            var userService = new UserService(_dbContextFactory, _userData, _mapper, _config);
            var emailService = new EmailService(configuration);
            var authController = new AuthenticationController(userService, configuration, emailService);
            string code = "209478";
            var result = await authController.ConfirmEmail(1, code);

            var user = await _fakeUserService.GetUserById(1);

            Assert.NotNull(user);
            Assert.True(user.EmailConfirmed);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Test_Login()
        {
            var configuration = InitConfiguration();
            var userService = new UserService(_dbContextFactory, _userData, _mapper, _config);
            var emailService = new EmailService(configuration);
            var authController = new AuthenticationController(userService, configuration, emailService);

            var user = new UserViewModel()
            {
                Email = "huythinh2507@gmail.com",
                Password = "testPassword123"
            };

            var result = await authController.Login(user);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}