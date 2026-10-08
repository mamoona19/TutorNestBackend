using TutorNest.API.DTOs;
using TutorNest.API.DTOs.NewFolder;

namespace TutorNest.API.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<SubjectDto>> GetSubjectsAsync();
    }
}