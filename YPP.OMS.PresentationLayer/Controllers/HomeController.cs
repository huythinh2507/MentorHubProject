using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.Services.TenantManagement;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/tenants")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly TenantService _tenantService;

        // Constructor injection
        public HomeController(TenantService tenantService)
        {
            _tenantService = tenantService;
        }

       
    }

}
