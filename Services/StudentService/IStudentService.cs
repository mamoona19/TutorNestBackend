using TutorNest.API.DTOs;

namespace TutorNest.API.Services.Student
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetStudentsByParentAsync(long parentId);
        Task<long?> GetParentIdByUserIdAsync(long userId);
        Task<long> CreateStudentAsync(
            long parentId,
            string studentName,
            int genderId,
            DateTime? dateOfBirth,
            int? classId,
            int? boardId,
            string? schoolName,
            int? preferredTutorGenderId,
            int? tuitionTypeId,
            string? learningNotes,
            int? academicClassId,
            long createdBy);

    }
}
