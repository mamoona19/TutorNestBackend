using TutorNest.API.DTOs;
using TutorNest.API.DTOs.City;

namespace TutorNest.API.Repositories.City
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityDto>> GetCitiesAsync();
    }
}