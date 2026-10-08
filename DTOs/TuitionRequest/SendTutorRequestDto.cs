namespace TutorNest.API.DTOs.TuitionRequest
{
    public class SendTutorRequestDto
    {
        
        public long TuitionRequestId { get; set; }

        public long TutorId { get; set; }

        public long CreatedBy { get; set; }
    }
}

