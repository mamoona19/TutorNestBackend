namespace TutorNest.API.Repositories.Auth_Repository
{
    public class RegisterUserResponseDto
    {
        public long UserId { get; set; }

        public int RoleId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}
