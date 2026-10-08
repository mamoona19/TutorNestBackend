using Dapper;
using System.Data;
using TutorNest.API.DTOs;
using TutorNest.API.DTOs.NewFolder;

namespace TutorNest.API.Repositories.Subject
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IDbConnection _connection;

        public SubjectRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync()
        {
            return await _connection.QueryAsync<SubjectDto>(
                "Common.usp_GetSubjects",
                commandType: CommandType.StoredProcedure);
        }
    }
}