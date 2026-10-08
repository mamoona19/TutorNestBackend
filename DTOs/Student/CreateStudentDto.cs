namespace TutorNest.API.DTOs.Student
{
    public class CreateStudentDto
    {
        public string StudentName { get; set; } = string.Empty;
        public int GenderId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? ClassId { get; set; }
        public int? BoardId { get; set; }
        public string? SchoolName { get; set; }
        public int? PreferredTutorGenderId { get; set; }
        public int? TuitionTypeId { get; set; }
        public string? LearningNotes { get; set; }
        public int? AcademicClassId { get; set; }
    }
}
