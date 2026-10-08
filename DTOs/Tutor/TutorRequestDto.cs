namespace TutorNest.API.DTOs.Tutor
{
    public class TutorRequestDto
    {
        public long TutorRequestId { get; set; }

        public long TuitionRequestId { get; set; }

        public long TutorId { get; set; }

        public int RequestStatusId { get; set; }

        public DateTime SentDate { get; set; }

        public DateTime? RespondedDate { get; set; }

        public string? TutorRemarks { get; set; }
    }
}
