using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Area;

namespace TutorNest.API.Services.Area
{
    public interface IAreaService
    {
        Task<IEnumerable<AreaDto>> GetAreasByCityAsync(int cityId);

    }
}