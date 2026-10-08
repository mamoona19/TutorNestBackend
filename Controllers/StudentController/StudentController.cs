using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TutorNest.API.DTOs.Student;
using TutorNest.API.Services.Student;

namespace TutorNest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [Authorize(Roles = "2")]
        [HttpGet("my-students")]
        public async Task<IActionResult> GetMyStudents()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found."
                });
            }

            var userId = long.Parse(userIdClaim.Value);

            // Get ParentId from logged-in UserId
            var parentId = await _service.GetParentIdByUserIdAsync(userId);

            if (parentId == null)
            {
                return BadRequest(new
                {
                    message = "Parent profile not found.",
                    userId = userId
                });
            }

            // Get students of this parent
            var students = await _service.GetStudentsByParentAsync(parentId.Value);

            return Ok(students);
        }
        [Authorize(Roles = "2")]
        [HttpPost]
        public async Task<IActionResult> CreateStudent(
    [FromBody] CreateStudentDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.StudentName))
            {
                return BadRequest(new
                {
                    message = "Student name is required."
                });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new
                {
                    message = "User ID not found."
                });
            }

            var userId = long.Parse(userIdClaim.Value);

            // Get ParentId from logged-in UserId
            var parentId = await _service.GetParentIdByUserIdAsync(userId);

            if (parentId == null)
            {
                return BadRequest(new
                {
                    message = "Parent profile not found.",
                    userId = userId
                });
            }

            // Create Student
            var studentId = await _service.CreateStudentAsync(
                parentId.Value,
                dto.StudentName,
                dto.GenderId,
                dto.DateOfBirth,
                dto.ClassId,
                dto.BoardId,
                dto.SchoolName,
                dto.PreferredTutorGenderId,
                dto.TuitionTypeId,
                dto.LearningNotes,
                dto.AcademicClassId,
                userId);

            return Ok(new
            {
                studentId,
                message = "Student created successfully."
            });
        }
    }
    }