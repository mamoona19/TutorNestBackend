namespace TutorNest.API.DTOs.TuitionRequest
{
    public class TutorRequestResponseDto
    {
        public long TutorRequestId { get; set; }
        public long TuitionRequestId { get; set; }
        public long TutorId { get; set; }
        public int RequestStatusId { get; set; }
        public string RequestStatusName { get; set; }
        public DateTime SentDate { get; set; }

        public DateTime? RespondedDate { get; set; }

        public string? TutorRemarks { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? UpdatedBy { get; set; }
    }
}
