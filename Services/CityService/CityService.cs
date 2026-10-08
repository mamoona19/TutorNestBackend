using TutorNest.API.DTOs;
using TutorNest.API.DTOs.City;
using TutorNest.API.Repositories.City;

namespace TutorNest.API.Services.City
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _repository;

        public CityService(ICityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CityDto>> GetCitiesAsync()
        {
            return await _repository.GetCitiesAsync();
        }
    }
}