namespace TutorNest.API.DTOs.Tutor
{
    public class TutorDemoRequestListDto
    {
        public long TutorDemoRequestId { get; set; }

        public long TuitionRequestId { get; set; }

        public long RequestTutorId { get; set; }

        public long TutorId { get; set; }

        public string StudentName { get; set; }

        public string SubjectName { get; set; }

        public string TuitionTypeName { get; set; }

        public string CityName { get; set; }

        public string AreaName { get; set; }

        public decimal Budget { get; set; }

        public DateTime? PreferredStartDate { get; set; }

        public string? ParentRemarks { get; set; }

        public DateTime RequestedDate { get; set; }

        public int DemoStatusId { get; set; }

        public string DemoStatusName { get; set; }
        public long? ScheduledAvailabilityId { get; set; }

        public DateTime? ScheduledDate { get; set; }

        public TimeSpan? ScheduledStartTime { get; set; }

        public TimeSpan? ScheduledEndTime { get; set; }
    }
}
