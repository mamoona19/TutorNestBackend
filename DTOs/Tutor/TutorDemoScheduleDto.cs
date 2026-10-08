namespace TutorNest.API.DTOs.Tutor
{
    public class TutorDemoScheduleDto
    {
        public long TutorDemoRequestId { get; set; }
        public long AvailabilityId { get; set; }
        public string? AdminRemarks { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
