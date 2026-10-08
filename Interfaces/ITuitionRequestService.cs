using TutorNest.API.DTOs.TuitionRequest;
using TutorNest.API.DTOs.Tutor;

namespace TutorNest.API.Interfaces
{
    public interface ITuitionRequestService
    {
        Task<CreateTuitionRequestResponseDto> CreateAsync(
            CreateTuitionRequestDto request);

        Task<TuitionRequestResponseDto?> GetByIdAsync(
            long tuitionRequestId);

        Task<IEnumerable<TuitionRequestResponseDto>> GetAllAsync();

        Task<int> UpdateStatusAsync(
            long id,
            UpdateTuitionRequestStatusDto request);
        Task<int> SendTutorRequestAsync(SendTutorRequestDto request);
        Task<int> UpdateTutorRequestStatusAsync( long tutorRequestId, int requestStatusId, long updatedBy);
        Task<IEnumerable<TutorRequestResponseDto>> GetTutorRequestsAsync();
        Task<IEnumerable<MyTuitionRequestResponseDto>> GetMyTuitionRequestsAsync(long parentId);
        Task<IEnumerable<TutorApplicationResponseDto>>GetTutorApplicationsByTuitionRequestAsync(long tuitionRequestId,long parentId);
        

    }
}