using TutorNest.API.DTOs;
using TutorNest.API.DTOs.NewFolder;
using TutorNest.API.Repositories;
using TutorNest.API.Repositories.Subject;

namespace TutorNest.API.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;

        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync()
        {
            return await _repository.GetSubjectsAsync();
        }
    }
}