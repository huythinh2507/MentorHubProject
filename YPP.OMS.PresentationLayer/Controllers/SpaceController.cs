using Microsoft.AspNetCore.Mvc;
using YPP.MH.BusinessLogicLayer.Services.SpaceService;
using YPP.MH.DataAccessLayer.Models;

namespace YPP.MH.PresentationLayer.Controllers
{
    [Route("api/spaces")]
    [ApiController]
    public class SpaceController : ControllerBase
    {
        private readonly SpaceService _spaceService;
        public SpaceController(SpaceService spaceService)
        {
            _spaceService = spaceService;
        }

        [HttpPost("spacegroup")]
        public async Task<IActionResult> CreateSpaceGroup([FromBody] SpaceGroup info)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var spaceGroup = await _spaceService.CreateSpaceGroup(info);
                return Ok(spaceGroup);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
