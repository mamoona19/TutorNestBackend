using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TutorNest.API.DTOs.LogIn;
using TutorNest.API.DTOs.Register;
using TutorNest.API.Services.Auth_Service;

namespace TutorNest.API.Controllers.AuthController
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            try
            {
                var result = await _service.RegisterAsync(dto);
                return Ok(result);
            }
            catch (SqlException ex) when (ex.Number == 50002)
            {
                return BadRequest(new
                {
                    message = "Email already registered."
                });
            }
            catch (SqlException ex) when (ex.Number == 50003)
            {
                return BadRequest(new
                {
                    message = "Phone number already registered."
                });

            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            var result = await _service.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(result);
        }
        [HttpPost("create-admin")]
        public async Task<IActionResult> CreateAdmin(
    RegisterUserDto dto)
        {
            try
            {
                var result =
                    await _service.CreateAdminAsync(dto);

                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = ex.Message
                });
            }
        }
    }
    }

