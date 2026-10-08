using Dapper;
using System.Data;
using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Tutor;

namespace TutorNest.API.Repositories.Parent
{
    public class ParentRepository : IParentRepository
    {
        private readonly IDbConnection _db;

        public ParentRepository(IDbConnection connection)
        {
            _db = connection;
        }

        public async Task<long?> GetParentIdByUserIdAsync(long userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await _db.QueryFirstOrDefaultAsync<long?>(
                "Parent.usp_GetParentIdByUserId",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<ParentDemoRequestListDto>> GetParentDemoRequestsAsync(long parentId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ParentId", parentId);

            return await _db.QueryAsync<ParentDemoRequestListDto>(
                "Parent.usp_GetParentDemoRequests",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}