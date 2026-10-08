using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Area;

namespace TutorNest.API.Repositories.Area
{
    public interface IAreaRepository
    {
        Task<IEnumerable<AreaDto>> GetAreasByCityAsync(int cityId);
    }
}