namespace TutorNest.API.DTOs.Tutor
{
    public class TutorDemoAvailabilityListDto
    {
        public long AvailabilityId { get; set; }
        public long TutorDemoRequestId { get; set; }
        public long TutorId { get; set; }

        public DateTime AvailableDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string? TutorRemarks { get; set; }
    }
}
