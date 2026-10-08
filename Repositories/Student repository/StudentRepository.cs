using Dapper;
using System.Data;
using TutorNest.API.DTOs;

namespace TutorNest.API.Repositories.Student
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IDbConnection _connection;

        public StudentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByParentAsync(
            long parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ParentId", parentId);

            return await _connection.QueryAsync<StudentDto>(
                "Parent.usp_GetStudentsByParent",
                parameters,
                commandType: CommandType.StoredProcedure);
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
            var parameters = new DynamicParameters();

            parameters.Add("@ParentId", parentId);
            parameters.Add("@StudentName", studentName);
            parameters.Add("@GenderId", genderId);
            parameters.Add("@DateOfBirth", dateOfBirth);
            parameters.Add("@ClassId", classId);
            parameters.Add("@BoardId", boardId);
            parameters.Add("@SchoolName", schoolName);
            parameters.Add("@PreferredTutorGenderId", preferredTutorGenderId);
            parameters.Add("@TuitionTypeId", tuitionTypeId);
            parameters.Add("@LearningNotes", learningNotes);
            parameters.Add("@AcademicClassId", academicClassId);
            parameters.Add("@CreatedBy", createdBy);

            return await _connection.ExecuteScalarAsync<long>(
                "Parent.usp_CreateStudent",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
        public async Task<long?> GetParentIdByUserIdAsync(long userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await _connection.QueryFirstOrDefaultAsync<long?>(
                "Parent.usp_GetParentIdByUserId",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}