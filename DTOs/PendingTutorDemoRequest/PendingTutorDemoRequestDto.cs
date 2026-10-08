namespace TutorNest.API.DTOs.PendingTutorDemoRequest
{
    public class PendingTutorDemoRequestDto
    {
        public long TutorDemoRequestId { get; set; }
        public long TuitionRequestId { get; set; }
        public long RequestTutorId { get; set; }

        public long ParentId { get; set; }
        public long TutorId { get; set; }

        public string TutorFirstName { get; set; }
        public string TutorLastName { get; set; }

        public long StudentId { get; set; }
        public string StudentName { get; set; }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public int TuitionTypeId { get; set; }
        public string TuitionTypeName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }

        public int AreaId { get; set; }
        public string AreaName { get; set; }

        public decimal Budget { get; set; }

        public DateTime? PreferredStartDate { get; set; }

        public string? ParentRemarks { get; set; }

        public DateTime RequestedDate { get; set; }

        public int DemoStatusId { get; set; }
        public string DemoStatusName { get; set; }
    }
}
