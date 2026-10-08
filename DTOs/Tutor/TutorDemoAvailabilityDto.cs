namespace TutorNest.API.DTOs.Tutor
{
    public class TutorDemoAvailabilityDto
    {
        public long TutorDemoRequestId { get; set; }

        public long TutorId { get; set; }

        public DateTime AvailableDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? TutorRemarks { get; set; }

        public long? CreatedBy { get; set; }
    }
}
