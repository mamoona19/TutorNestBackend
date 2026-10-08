using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Area;
using TutorNest.API.Repositories.Area;

namespace TutorNest.API.Services.Area
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AreaDto>> GetAreasByCityAsync(int cityId)
        {
            return await _repository.GetAreasByCityAsync(cityId);
        }
    }
}