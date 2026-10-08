namespace TutorNest.API.DTOs.TuitionRequest
{
    public class TutorApplicationResponseDto
    {
        public long RequestTutorId { get; set; }

        public long TuitionRequestId { get; set; }

        public long TutorId { get; set; }

        public long UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int RequestStatusId { get; set; }

        public string RequestStatusName { get; set; } = string.Empty;

        public string? TutorRemarks { get; set; }

        public bool IsSelected { get; set; }

        public DateTime CreatedDate { get; set; }

        // Demo information
        public bool DemoRequested { get; set; }

        public int? DemoStatusId { get; set; }

        public string? DemoStatusName { get; set; }
    }
}