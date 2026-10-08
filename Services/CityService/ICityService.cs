using TutorNest.API.DTOs;
using TutorNest.API.DTOs.City;

namespace TutorNest.API.Services.City
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCitiesAsync();
    }
}