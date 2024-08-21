using Microsoft.Extensions.DependencyInjection;
using YPP.MH.DataAccessLayer.Models;
using YPP.MH.DataAccessLayer.Repositories.Base;
using YPP.MH.DataAccessLayer.Repositories.UnitOfWork;

namespace YPP.MH.DataAccessLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
