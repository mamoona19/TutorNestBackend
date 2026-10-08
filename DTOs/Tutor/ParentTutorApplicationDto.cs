namespace TutorNest.API.DTOs.Tutor
{
    public class ParentTutorApplicationDto
    {
        public long RequestTutorId { get; set; }
        public long TuitionRequestId { get; set; }
        public long TutorId { get; set; }

        public string? TutorName { get; set; }
        public string? TutorRemarks { get; set; }

        public int RequestStatusId { get; set; }
        public string? ApplicationStatus { get; set; }

        public bool IsSelected { get; set; }

        public DateTime SentDate { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}
