namespace TutorNest.API.DTOs.LogIn
{
    public class LoginUserResponseDto
    {
        public long UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int RoleId { get; set; }

        public string Message { get; set; } = string.Empty;
        public string Token { get; set; }
    }
}
