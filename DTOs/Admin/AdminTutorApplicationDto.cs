namespace TutorNest.API.DTOs.Admin
{
    public class AdminTutorApplicationDto
    {
        public long RequestTutorId { get; set; }
        public long TuitionRequestId { get; set; }
        public long TutorId { get; set; }

        public int RequestStatusId { get; set; }
        public string? ApplicationStatus { get; set; }

        public DateTime SentDate { get; set; }
        public DateTime? ResponseDate { get; set; }

        public string? AdminRemarks { get; set; }
        public string? TutorRemarks { get; set; }

        public bool IsSelected { get; set; }

        public long StudentId { get; set; }

        public int TuitionTypeId { get; set; }
        public string? TuitionTypeName { get; set; }

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public int CityId { get; set; }
        public string? CityName { get; set; }

        public int AreaId { get; set; }
        public string? AreaName { get; set; }

        public decimal Budget { get; set; }

        public DateTime PreferredStartDate { get; set; }

        public string? SpecialInstructions { get; set; }

        public int TuitionRequestStatusId { get; set; }
        public string? TuitionRequestStatus { get; set; }
    }
}
