using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorNest.API.DTOs.TuitionRequest;
using TutorNest.API.Interfaces;
using TutorNest.API.Services.Parent;
using TutorNest.API.Interfaces;
using TutorNest.API.DTOs;

using TutorNest.API.Repositories;

namespace TutorNest.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class TuitionRequestController : ControllerBase
    {
        private readonly ITuitionRequestService _service;
        private readonly IParentService _parentService;

        public TuitionRequestController(
            ITuitionRequestService service,
            IParentService parentService)
        {
            _service = service;
            _parentService = parentService;
        }

        // Create Tuition Request
        [HttpPost]
        public async Task<IActionResult> Create(CreateTuitionRequestDto request)
        {
            var result = await _service.CreateAsync(request);

            return Ok(result);
        }

        // Get Tuition Request By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    message = "Tuition Request not found."
                });
            }

            return Ok(result);
        }

        // Get All Tuition Requests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        // Get Logged-in Parent's Tuition Requests
        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user."
                });
            }

            var parentId = await _parentService.GetParentIdByUserIdAsync(userId);

            if (parentId == null)
            {
                return NotFound(new
                {
                    message = "Parent profile not found."
                });
            }

            var result = await _service.GetMyTuitionRequestsAsync(parentId.Value);

            return Ok(result);
        }

        // Update Tuition Request Status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(
            long id,
            UpdateTuitionRequestStatusDto request)
        {
            var result = await _service.UpdateStatusAsync(id, request);

            if (result == 0)
            {
                return NotFound(new
                {
                    message = "Tuition Request not found."
                });
            }

            if (result == 2)
            {
                return BadRequest(new
                {
                    message = "Invalid StatusId. Please provide a valid active status."
                });
            }

            if (result == 3)
            {
                return BadRequest(new
                {
                    message = "Status transition is not allowed."
                });
            }

            return Ok(new
            {
                message = "Tuition Request Status updated successfully."
            });
        }

        // Send Tutor Request
        [HttpPost("send-tutor")]
        public async Task<IActionResult> SendTutorRequest(
            SendTutorRequestDto request)
        {
            var result = await _service.SendTutorRequestAsync(request);

            if (result == 0)
            {
                return NotFound(new
                {
                    message = "Tuition Request not found."
                });
            }

            if (result == 2)
            {
                return NotFound(new
                {
                    message = "Tutor not found."
                });
            }

            if (result == 3)
            {
                return BadRequest(new
                {
                    message = "Tutor Request already exists."
                });
            }

            return Ok(new
            {
                message = "Tutor Request sent successfully."
            });
        }

        // Update Tutor Request Status
        [HttpPut("tutor-request/{tutorRequestId}/status")]
        public async Task<IActionResult> UpdateTutorRequestStatus(
            long tutorRequestId,
            int requestStatusId,
            long updatedBy)
        {
            var result = await _service.UpdateTutorRequestStatusAsync(
                tutorRequestId,
                requestStatusId,
                updatedBy);

            if (result == 0)
            {
                return NotFound(new
                {
                    message = "Tutor Request not found."
                });
            }

            if (result == 2)
            {
                return BadRequest(new
                {
                    message = "Invalid Request Status."
                });
            }

            if (result == 3)
            {
                return BadRequest(new
                {
                    message = "Tutor Request is not in Pending status."
                });
            }

            return Ok(new
            {
                message = "Tutor Request status updated successfully."
            });
        }

        // Get Tutor Requests
        [HttpGet("tutor-requests")]
        public async Task<IActionResult> GetTutorRequests()
        {
            var result = await _service.GetTutorRequestsAsync();

            return Ok(result);
        }
        [HttpGet("{tuitionRequestId}/tutor-applications")]
        public async Task<IActionResult> GetTutorApplications(
    long tuitionRequestId)
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!long.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new
                {
                    message = "Invalid user."
                });
            }

            var parentId =
                await _parentService.GetParentIdByUserIdAsync(userId);

            if (parentId == null)
            {
                return NotFound(new
                {
                    message = "Parent profile not found."
                });
            }

            var result =
                await _service.GetTutorApplicationsByTuitionRequestAsync(
                    tuitionRequestId,
                    parentId.Value);

            return Ok(result);
        }
    }
}