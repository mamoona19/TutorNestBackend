using TutorNest.API.DTOs.LogIn;
using TutorNest.API.DTOs.Register;
using TutorNest.API.Repositories.Auth_Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TutorNest.API.Services.Auth_Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            // Password ko hash karna hai
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // Database mein hashed password bhejna
            return await _repository.RegisterAsync(dto, passwordHash);
        }

        public async Task<LoginUserResponseDto?> LoginAsync(LoginUserDto dto)
        {
            var user = await _repository.LoginAsync(dto.Email);

            if (user == null)
            {
                return null;
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            // JWT Claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            // JWT Secret Key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            // JWT Token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    Convert.ToDouble(
                        _configuration["Jwt:ExpiryMinutes"]
                    )
                ),
                signingCredentials: credentials
            );

            string tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return new LoginUserResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId,
                Token = tokenString,
                Message = "Login successful."
            };
        }
        public async Task<RegisterUserResponseDto> CreateAdminAsync(RegisterUserDto dto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            return await _repository.CreateAdminAsync( dto, passwordHash);
        }
    }
}