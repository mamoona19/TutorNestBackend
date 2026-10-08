using TutorNest.API.DTOs;
using TutorNest.API.Repositories.Student;

namespace TutorNest.API.Services.Student
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByParentAsync(
            long parentId)
        {
            return await _repository.GetStudentsByParentAsync(parentId);
        }
        public async Task<long> CreateStudentAsync(
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
    long createdBy)
        {
            return await _repository.CreateStudentAsync(
                parentId,
                studentName,
                genderId,
                dateOfBirth,
                classId,
                boardId,
                schoolName,
                preferredTutorGenderId,
                tuitionTypeId,
                learningNotes,
                academicClassId,
                createdBy);
        }
        public async Task<long?> GetParentIdByUserIdAsync(long userId)
        {
            return await _repository.GetParentIdByUserIdAsync(userId);
        }
    }
}