using TutorNest.API.DTOs;
using TutorNest.API.DTOs.NewFolder;

namespace TutorNest.API.Services.Subject
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetSubjectsAsync();
    }
}