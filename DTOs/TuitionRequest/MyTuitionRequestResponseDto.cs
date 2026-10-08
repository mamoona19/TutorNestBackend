namespace TutorNest.API.DTOs.TuitionRequest
{
    public class MyTuitionRequestResponseDto
    {
        public long TuitionRequestId { get; set; }
        public long ParentId { get; set; }

        public long StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public int TuitionTypeId { get; set; }
        public string TuitionTypeName { get; set; } = string.Empty;

        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

        public int CityId { get; set; }
        public string CityName { get; set; } = string.Empty;

        public int AreaId { get; set; }
        public string AreaName { get; set; } = string.Empty;

        public decimal Budget { get; set; }

        public int? PreferredTutorGenderId { get; set; }

        public DateTime? PreferredStartDate { get; set; }

        public string? SpecialInstructions { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}