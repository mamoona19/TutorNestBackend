using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorNest.API.Services.Area;

namespace TutorNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _service;

        public AreaController(IAreaService service)
        {
            _service = service;
        }

        [HttpGet("by-city/{cityId}")]
        public async Task<IActionResult> GetAreasByCity(int cityId)
        {
            var areas = await _service.GetAreasByCityAsync(cityId);

            return Ok(areas);
        }
    }
}