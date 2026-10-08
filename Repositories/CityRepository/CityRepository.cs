using Dapper;
using System.Data;
using TutorNest.API.DTOs;
using TutorNest.API.DTOs.City;

namespace TutorNest.API.Repositories.City
{
    public class CityRepository : ICityRepository
    {
        private readonly IDbConnection _connection;

        public CityRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<CityDto>> GetCitiesAsync()
        {
            return await _connection.QueryAsync<CityDto>(
                "Common.usp_GetCity",
                commandType: CommandType.StoredProcedure);
        }
    }
}