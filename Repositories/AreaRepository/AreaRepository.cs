using Dapper;
using System.Data;
using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Area;

namespace TutorNest.API.Repositories.Area
{
    public class AreaRepository : IAreaRepository
    {
        private readonly IDbConnection _connection;

        public AreaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<AreaDto>> GetAreasByCityAsync(int cityId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@CityId", cityId);

            return await _connection.QueryAsync<AreaDto>(
                "Common.usp_GetAreasByCity",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }
}