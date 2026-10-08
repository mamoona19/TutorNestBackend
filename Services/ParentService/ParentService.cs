using TutorNest.API.DTOs.Tutor;
using TutorNest.API.Repositories.Parent;

namespace TutorNest.API.Services.Parent
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _repository;

        public ParentService(IParentRepository repository)
        {
            _repository = repository;
        }

        public async Task<long?> GetParentIdByUserIdAsync(long userId)
        {
            return await _repository.GetParentIdByUserIdAsync(userId);
        }
        public async Task<IEnumerable<ParentDemoRequestListDto>>
    GetParentDemoRequestsAsync(long parentId)
        {
            return await _repository
                .GetParentDemoRequestsAsync(parentId);
        }
    }
}