using TutorNest.API.DTOs;
using TutorNest.API.DTOs.TuitionRequest;
using TutorNest.API.Interfaces;
using TutorNest.API.Repositories;
namespace TutorNest.API.Services.TuitionService
{
    public class TuitionRequestService : ITuitionRequestService
    {
        private ITuitionRequestRepository _repository;
        public TuitionRequestService(ITuitionRequestRepository repository)
        {
            _repository=repository;
        }
        //public CreateTuitionRequestDto Create(CreateTuitionRequestDto request)
        //{
        //    return request;
        //}
        public async Task<CreateTuitionRequestResponseDto> CreateAsync(CreateTuitionRequestDto request)
        {
            return await _repository.CreateAsync(request);

        }
        public async Task<TuitionRequestResponseDto?> GetByIdAsync (long tuitionRequestId)
        {
            return await _repository.GetByIdAsync(tuitionRequestId);
        }
        public async Task<IEnumerable<TuitionRequestResponseDto>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<int> UpdateStatusAsync(long id, UpdateTuitionRequestStatusDto request)
        {
            return await  _repository.UpdateStatusAsync(id, request);
        }
        public async Task<int> SendTutorRequestAsync(SendTutorRequestDto request)
        {
            return await _repository.SendTutorRequestAsync(request);
        }
        public async Task<int> UpdateTutorRequestStatusAsync( long tutorRequestId,int requestStatusId,long updatedBy)
        {
            return await _repository.UpdateTutorRequestStatusAsync(tutorRequestId, requestStatusId,updatedBy);
        }
        public async Task<IEnumerable<TutorRequestResponseDto>> GetTutorRequestsAsync()
        {
            return await _repository.GetTutorRequestsAsync();

        }
        public async Task<IEnumerable<MyTuitionRequestResponseDto>> GetMyTuitionRequestsAsync(long parentId)
        {
            return await _repository.GetMyTuitionRequestsAsync(parentId);
        }
        public async Task<IEnumerable<TutorApplicationResponseDto>>
    GetTutorApplicationsByTuitionRequestAsync(
        long tuitionRequestId,
        long parentId)
        {
            return await _repository
                .GetTutorApplicationsByTuitionRequestAsync(
                    tuitionRequestId,
                    parentId);
        }
    }
    }
