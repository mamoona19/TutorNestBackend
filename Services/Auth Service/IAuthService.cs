using TutorNest.API.DTOs.LogIn;
using TutorNest.API.DTOs.Register;
using TutorNest.API.Repositories.Auth_Repository;

namespace TutorNest.API.Services.Auth_Service
{
    public interface IAuthService
    {
        Task<RegisterUserResponseDto> RegisterAsync(RegisterUserDto dto);
        Task<LoginUserResponseDto?> LoginAsync(LoginUserDto dto);
        Task<RegisterUserResponseDto> CreateAdminAsync( RegisterUserDto dto);
    }
}
