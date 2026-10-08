using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorNest.API.Services.Parent;

namespace TutorNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ParentController : ControllerBase
    {
        private readonly IParentService _service;

        public ParentController(IParentService service)
        {
            _service = service;
        }

        [HttpGet("my-parent-id")]
        public async Task<IActionResult> GetMyParentId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user.");
            }

            var parentId = await _service.GetParentIdByUserIdAsync(userId);

            if (parentId == null)
            {
                return NotFound("Parent profile not found.");
            }

            return Ok(new
            {
                parentId = parentId
            });
        }
        [Authorize(Roles = "2")]
        [HttpGet("my-demo-requests")]
        public async Task<IActionResult> GetMyDemoRequests()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "User identity not found."
                });
            }

            if (!long.TryParse(userIdClaim, out long userId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Invalid user identity."
                });
            }

            var parentId = await _service
                .GetParentIdByUserIdAsync(userId);

            if (!parentId.HasValue)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Parent profile not found."
                });
            }

            var demos = await _service
                .GetParentDemoRequestsAsync(parentId.Value);

            return Ok(demos);
        }
    }
}