using Microsoft.Extensions.DependencyInjection;
using YPP.MH.BusinessLogicLayer.Services.PostManagement;
using YPP.MH.BusinessLogicLayer.Services.UserManagement;
using YPP.MH.DataAccessLayer.Repositories.UnitOfWork;

namespace YPP.MH.BusinessLogicLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
