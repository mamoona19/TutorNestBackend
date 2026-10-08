namespace TutorNest.API.DTOs.Tutor
{
    public class AvailableTuitionRequestDto
    {
        public long TuitionRequestId { get; set; }
        public long ParentId { get; set; }
        public long StudentId { get; set; }
        public int TuitionTypeId { get; set; }
        public int CityId { get; set; }
        public int AreaId { get; set; }
        public decimal Budget { get; set; }
        public DateTime? PreferredStartDate { get; set; }
        public string? SpecialInstructions { get; set; }
        public int StatusId { get; set; }
        public int SubjectId { get; set; }
    }
}