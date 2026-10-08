using TutorNest.API.DTOs.Tutor;

namespace TutorNest.API.Services.Parent
{
    public interface IParentService
    {
        Task<long?> GetParentIdByUserIdAsync(long userId);
        Task<IEnumerable<ParentDemoRequestListDto>> GetParentDemoRequestsAsync(long parentId);
    }
}