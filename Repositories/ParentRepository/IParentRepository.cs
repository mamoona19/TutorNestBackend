using TutorNest.API.DTOs;
using TutorNest.API.DTOs.Tutor;

namespace TutorNest.API.Repositories.Parent
{
    public interface IParentRepository
    {
        Task<long?> GetParentIdByUserIdAsync(long userId);
        Task<IEnumerable<ParentDemoRequestListDto>> GetParentDemoRequestsAsync(long parentId);
    }
}