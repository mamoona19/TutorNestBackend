namespace TutorNest.API.DTOs.TuitionRequest
{
    public class CreateTuitionRequestDto
    {
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
        public long CreatedBy { get; set; }
    }
}