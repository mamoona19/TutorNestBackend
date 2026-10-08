using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TutorNest.API.Services.City;

namespace TutorNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _service.GetCitiesAsync();

            return Ok(cities);
        }
    }
}