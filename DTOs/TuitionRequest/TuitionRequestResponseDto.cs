namespace TutorNest.API.DTOs.TuitionRequest
{
    public class TuitionRequestResponseDto
    {
        public long TuitionRequestId { get; set; }
        public long ParentId { get; set; }
        public long StudentId { get; set; }
        public int TuitionTypeId { get; set; }
        public int SubjectId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public decimal Budget { get; set; }
        public int? PreferredTutorGenderId { get; set; }
        public DateTime? PreferredStartDate { get; set; }
        public string? SpecialInstructions { get; set; }
        public int StatusId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
