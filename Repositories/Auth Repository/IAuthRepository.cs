using TutorNest.API.DTOs.LogIn;
using TutorNest.API.DTOs.Register;

namespace TutorNest.API.Repositories.Auth_Repository
{
    public interface IAuthRepository
    {
        Task<RegisterUserResponseDto> RegisterAsync(RegisterUserDto dto, string passwordHash);
        Task<LoginUserDbDto?> LoginAsync(string email);
        Task<RegisterUserResponseDto> CreateAdminAsync( RegisterUserDto dto, string passwordHash);
    }
}
