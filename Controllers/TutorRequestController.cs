using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using TutorNest.API.DTOs.Tutor;
using TutorNest.API.Interfaces;
using TutorNest.API.Services;
using TutorNest.API.Services.Parent;

namespace TutorNest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorRequestController : ControllerBase
    {
        private readonly ITutorRequestService _service;
        private readonly IParentService _parentService;

        public TutorRequestController(ITutorRequestService service, IParentService parentService)
        {
            _service = service;
            _parentService = parentService;
        }

        // ==============================
        // TUTOR REQUESTS
        // ==============================

        [Authorize(Roles = "3")]
        [HttpGet]
        public async Task<IActionResult> GetTutorRequests()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized();
            }

            if (!long.TryParse(userIdClaim, out long userId))
            {
                return Unauthorized();
            }

            var requests =
                await _service.GetTutorRequestsAsync(userId);

            return Ok(requests);
        }


        // ==============================
        // ACCEPT TUTOR REQUEST
        // ==============================

        [Authorize(Roles = "3")]
        [HttpPut("{requestTutorId}/accept")]
        public async Task<IActionResult> AcceptTutorRequest(
            long requestTutorId,
            [FromBody] string? tutorRemarks)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out long userId))
            {
                return Unauthorized();
            }

            var tutorId =
                await _service.GetTutorIdByUserIdAsync(userId);

            if (tutorId == null)
            {
                return Unauthorized("Tutor profile not found.");
            }

            var result =
                await _service.AcceptTutorRequestAsync(
                    requestTutorId,
                    tutorId.Value,
                    2,
                    tutorRemarks,
                    userId
                );

            return Ok(new
            {
                message = result
            });
        }


        // ==============================
        // AVAILABLE TUITION REQUESTS
        // ==============================

        [Authorize(Roles = "3")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableTuitionRequests()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out long userId))
            {
                return Unauthorized();
            }

            var tutorId =
                await _service.GetTutorIdByUserIdAsync(userId);

            if (tutorId == null)
            {
                return NotFound("Tutor profile not found.");
            }

            var requests =
                await _service.GetAvailableTuitionRequestsAsync(
                    tutorId.Value);

            return Ok(requests);
        }


        // ==============================
        // APPLY FOR TUITION REQUEST
        // ==============================

        [Authorize(Roles = "3")]
        [HttpPost("apply")]
        public async Task<IActionResult> ApplyForTuitionRequest(
            long tuitionRequestId,
            string? tutorRemarks)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!long.TryParse(userIdClaim, out long userId))
                {
                    return Unauthorized();
                }

                var tutorId =
                    await _service.GetTutorIdByUserIdAsync(userId);

                if (tutorId == null)
                {
                    return NotFound("Tutor profile not found.");
                }

                var result =
                    await _service.ApplyForTuitionRequestAsync(
                        tuitionRequestId,
                        tutorId.Value,
                        tutorRemarks,
                        userId
                    );

                return Ok(new
                {
                    message = "Applied successfully.",
                    requestTutorId = result
                });
            }
            catch (SqlException ex) when (ex.Number == 50002)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }


        // ==============================
        // MY APPLICATIONS
        // ==============================

        [Authorize(Roles = "3")]
        [HttpGet("my-applications")]
        public async Task<IActionResult> GetMyTutorApplications()
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!long.TryParse(userIdClaim, out long userId))
                {
                    return Unauthorized();
                }

                var tutorId =
                    await _service.GetTutorIdByUserIdAsync(userId);

                if (tutorId == null)
                {
                    return NotFound("Tutor profile not found.");
                }

                var applications =
                    await _service.GetMyTutorApplicationsAsync(
                        tutorId.Value);

                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }


        // ==============================
        // ADMIN - ALL TUTOR APPLICATIONS
        // ==============================

        [Authorize(Roles = "1")]
        [HttpGet("admin-applications")]
        public async Task<IActionResult> GetTutorApplicationsForAdmin()
        {
            try
            {
                var applications =
                    await _service.GetTutorApplicationsForAdminAsync();

                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }


        // ==============================
        // TEMPORARY AUTH DEBUG
        // REMOVE AFTER TESTING
        // ==============================

        [Authorize(Roles = "1")]
        [HttpGet("debug-auth")]
        public IActionResult DebugAuth()
        {
            return Ok(new
            {
                isAuthenticated =
                    User.Identity?.IsAuthenticated,

                authenticationType =
                    User.Identity?.AuthenticationType,

                name =
                    User.Identity?.Name,

                roles =
                    User.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList(),

                claims =
                    User.Claims.Select(c => new
                    {
                        c.Type,
                        c.Value
                    }).ToList()
            });
        }
        [Authorize(Roles = "1")]
        [HttpPut("admin-applications/{requestTutorId}/status")]
        public async Task<IActionResult> AdminUpdateTutorApplication(
    long requestTutorId,
    [FromQuery] int requestStatusId,
    [FromQuery] string? adminRemarks)
        {
            var updatedByClaim = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (!long.TryParse(updatedByClaim, out long updatedBy))
            {
                return Unauthorized();
            }

            var result = await _service.AdminUpdateTutorApplicationAsync(
                requestTutorId,
                requestStatusId,
                adminRemarks,
                updatedBy
            );

            return Ok(new
            {
                message = result
            });
        }
        [Authorize(Roles = "2")]
        [HttpGet("parent-accepted")]
        public async Task<IActionResult> GetAcceptedTutorApplicationsForParent()
        {
            var userIdClaim = User.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier
            )?.Value;

            if (!long.TryParse(userIdClaim, out long userId))
                return Unauthorized();

            var parentId = await _parentService
                .GetParentIdByUserIdAsync(userId);

            if (!parentId.HasValue)
                return NotFound("Parent profile not found.");

            var result = await _service
                .GetAcceptedTutorApplicationsForParentAsync(parentId.Value);

            return Ok(result);
        }
        [Authorize(Roles = "2")]
        [HttpPost("request-demo")]
        public async Task<IActionResult> RequestDemo(
    [FromBody] TutorDemoRequestDto request)
        {
            try
            {
                var result =
                    await _service.CreateTutorDemoRequestAsync(request);

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Demo request already exists."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Demo request submitted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to submit demo request.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "1")]
        [HttpGet("admin-pending-demos")]
        public async Task<IActionResult> GetPendingTutorDemoRequests()
        {
            try
            {
                var result =
                    await _service.GetPendingTutorDemoRequestsAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to load pending demo requests.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "3")]
        [HttpPost("demo-availability")]
        public async Task<IActionResult> AddTutorDemoAvailability(
    [FromBody] TutorDemoAvailabilityDto request)
        {
            try
            {
                var result =
                    await _service.AddTutorDemoAvailabilityAsync(request);

                if (result == null || result == 0)
                {
                    return BadRequest(new
                    {
                        message = "This availability slot already exists."
                    });
                }

                return Ok(new
                {
                    success = true,
                    availabilityId = result,
                    message = "Demo availability submitted successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to submit demo availability.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "3")]
        [HttpGet("my-demo-requests")]
        public async Task<IActionResult> GetMyDemoRequests()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? User.FindFirst("sub")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new
                    {
                        message = "User identity not found."
                    });
                }

                var tutorId = await _service.GetTutorIdByUserIdAsync(userId);

                if (!tutorId.HasValue)
                {
                    return Unauthorized(new
                    {
                        message = "Tutor profile not found."
                    });
                }

                var result =
                    await _service.GetTutorDemoRequestsAsync(tutorId.Value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to load demo requests.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "1")]
        [HttpPost("admin-schedule-demo")]
        public async Task<IActionResult> ScheduleTutorDemo(
    [FromBody] TutorDemoScheduleDto request)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new
                    {
                        message = "Admin identity not found."
                    });
                }

                request.UpdatedBy = userId;

                var result =
                    await _service.ScheduleTutorDemoAsync(request);

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Unable to schedule demo."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Demo scheduled successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to schedule demo.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "1")]
        [HttpGet("admin-demo-availability/{tutorDemoRequestId}")]
        public async Task<IActionResult> GetTutorDemoAvailability(
    long tutorDemoRequestId)
        {
            try
            {
                var result =
                    await _service.GetTutorDemoAvailabilityAsync(
                        tutorDemoRequestId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to load demo availability.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "3")]
        [HttpPut("tutor-confirm-demo/{tutorDemoRequestId}")]
        public async Task<IActionResult> TutorConfirmDemo(
    long tutorDemoRequestId)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new
                    {
                        message = "Tutor identity not found."
                    });
                }

                // JWT UserId → TutorId
                var tutorId =
                    await _service.GetTutorIdByUserIdAsync(userId);

                if (!tutorId.HasValue)
                {
                    return Unauthorized(new
                    {
                        message = "Tutor profile not found."
                    });
                }

                var result =
                    await _service.TutorConfirmDemoAsync(
                        tutorDemoRequestId,
                        tutorId.Value);

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Unable to confirm demo."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Demo availability confirmed successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to confirm demo.",
                    error = ex.Message
                });
            }
        }
        [Authorize(Roles = "1")]
        [HttpPut("admin-confirm-parent-demo/{tutorDemoRequestId}")]
        public async Task<IActionResult> AdminConfirmParentDemo(
    long tutorDemoRequestId)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new
                    {
                        message = "Admin identity not found."
                    });
                }

                var result =
                    await _service.AdminConfirmParentDemoAsync(
                        tutorDemoRequestId,
                        userId);

                if (!result)
                {
                    return BadRequest(new
                    {
                        message = "Unable to confirm parent."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Demo confirmed with parent successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Unable to confirm parent.",
                    error = ex.Message
                });
            }
        }
        [HttpPut("complete-demo")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> CompleteTutorDemo(
    [FromQuery] long tutorDemoRequestId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "User identity not found."
                });
            }

            var tutorId = await _service
                .GetTutorIdByUserIdAsync(userId);

            if (!tutorId.HasValue)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Tutor identity not found."
                });
            }

            var result = await _service
                .CompleteTutorDemoAsync(
                    tutorDemoRequestId,
                    userId
                );

            return Ok(new
            {
                success = result,
                message = result
                    ? "Demo marked as completed successfully."
                    : "Demo could not be completed."
            });
        }
        [Authorize(Roles = "1")]
        [HttpPut("select-tutor/{requestTutorId}")]
        public async Task<IActionResult> SelectTutor(
    long requestTutorId)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                if (!long.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Admin identity not found."
                    });
                }

                var result = await _service.SelectTutorAsync(
                    requestTutorId,
                    userId
                );

                if (!result)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Unable to select tutor."
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Tutor selected successfully."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Unable to select tutor.",
                    error = ex.Message
                });
            }
        }
    }
    }