namespace TutorNest.API.DTOs.Tutor
{
    public class TutorDemoRequestDto
    {
        public long TuitionRequestId { get; set; }
        public long RequestTutorId { get; set; }
        public long ParentId { get; set; }
        public long TutorId { get; set; }
        public string? ParentRemarks { get; set; }
    }
}
