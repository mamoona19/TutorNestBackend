using Dapper;
using System.Data;
using System.Runtime.InteropServices;
using TutorNest.API.DTOs.LogIn;
using TutorNest.API.DTOs.Register;

namespace TutorNest.API.Repositories.Auth_Repository
{
    public class AuthRepository:IAuthRepository
    {
        private readonly IDbConnection _connection;
        public AuthRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<RegisterUserResponseDto> RegisterAsync(RegisterUserDto dto, string passwordHash)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", dto.FirstName);
            parameters.Add("@LastName", dto.LastName);
            parameters.Add("@Email", dto.Email);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            parameters.Add("@PasswordHash", passwordHash);
            parameters.Add("@RoleId", dto.RoleId);
            var result = await _connection.QuerySingleAsync<RegisterUserResponseDto>
                (
                "Auth.usp_RegisterUser", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<LoginUserDbDto?> LoginAsync(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            var result=await _connection.QuerySingleOrDefaultAsync<LoginUserDbDto>(
                "Auth.usp_LoginUser",parameters, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<RegisterUserResponseDto> CreateAdminAsync(
    RegisterUserDto dto,
    string passwordHash)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@FirstName", dto.FirstName);
            parameters.Add("@LastName", dto.LastName);
            parameters.Add("@Email", dto.Email);
            parameters.Add("@PhoneNumber", dto.PhoneNumber);
            parameters.Add("@PasswordHash", passwordHash);

            return await _connection.QuerySingleAsync<RegisterUserResponseDto>(
                "Auth.usp_CreateAdminUser",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
