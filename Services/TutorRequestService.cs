using TutorNest.API.DTOs.Admin;
using TutorNest.API.DTOs.MyTutorApplication;
using TutorNest.API.DTOs.PendingTutorDemoRequest;
using TutorNest.API.DTOs.Tutor;

using TutorNest.API.Interfaces;
using TutorNest.API.Repositories.TutorRequest;

namespace TutorNest.API.Services
{
    public class TutorRequestService : ITutorRequestService
    {
        private readonly ITutorRequestRepository _repository;

        public TutorRequestService(ITutorRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TutorRequestDto>> GetTutorRequestsAsync(long userId)
        {
            return await _repository.GetTutorRequestsAsync(userId);
        }
        public async Task<string> AcceptTutorRequestAsync(long requestTutorId,long tutorId,int requestStatusId, string? tutorRemarks, long updatedBy)
        {
            return await _repository.AcceptTutorRequestAsync(  requestTutorId, tutorId,requestStatusId, tutorRemarks, updatedBy);
        }

        public async Task<string> RejectTutorRequestAsync(long requestTutorId, long tutorId,int requestStatusId, string? tutorRemarks, long updatedBy)
        {
            return await _repository.RejectTutorRequestAsync(  requestTutorId, tutorId, requestStatusId, tutorRemarks, updatedBy);
        }
        public async Task<long?> GetTutorIdByUserIdAsync(long userId)
        {
            return await _repository.GetTutorIdByUserIdAsync(userId);
        }
        public async Task<IEnumerable<AvailableTuitionRequestDto>> GetAvailableTuitionRequestsAsync(long tutorId)
        {
            return await _repository.GetAvailableTuitionRequestsAsync(tutorId);
        }
        public async Task<long> ApplyForTuitionRequestAsync( long tuitionRequestId,long tutorId, string? tutorRemarks, long createdBy)
        {
            return await _repository.ApplyForTuitionRequestAsync( tuitionRequestId, tutorId, tutorRemarks, createdBy);
        }
        public async Task<IEnumerable<MyTutorApplicationDto>> GetMyTutorApplicationsAsync(long tutorId)
        {
            return await _repository.GetMyTutorApplicationsAsync(tutorId);
        }
        public async Task<IEnumerable<AdminTutorApplicationDto>>GetTutorApplicationsForAdminAsync()
        {
            return await _repository .GetTutorApplicationsForAdminAsync();
        }
        public async Task<string> AdminUpdateTutorApplicationAsync(
    long requestTutorId,
    int requestStatusId,
    string? adminRemarks,
    long updatedBy)
        {
            return await _repository.AdminUpdateTutorApplicationAsync(
                requestTutorId,
                requestStatusId,
                adminRemarks,
                updatedBy
            );
        }
        public async Task<IEnumerable<ParentTutorApplicationDto>> GetAcceptedTutorApplicationsForParentAsync(long parentId)
        {
            return await _repository.GetAcceptedTutorApplicationsForParentAsync(parentId);
        }
        public async Task<bool> CreateTutorDemoRequestAsync(TutorDemoRequestDto request)
        {
            return await _repository.CreateTutorDemoRequestAsync(request);
        }
        public async Task<IEnumerable<PendingTutorDemoRequestDto>> GetPendingTutorDemoRequestsAsync()
        {
            return await _repository .GetPendingTutorDemoRequestsAsync();
        }
        public async Task<long?> AddTutorDemoAvailabilityAsync( TutorDemoAvailabilityDto request)
        {
            return await _repository .AddTutorDemoAvailabilityAsync(request);
        }
        public async Task<IEnumerable<TutorDemoRequestListDto>> GetTutorDemoRequestsAsync(long tutorId)
        {
            return await _repository .GetTutorDemoRequestsAsync(tutorId);
        }
        public async Task<bool> ScheduleTutorDemoAsync( TutorDemoScheduleDto request)
        {
            return await _repository .ScheduleTutorDemoAsync(request);
        }
        public async Task<IEnumerable<TutorDemoAvailabilityListDto>>GetTutorDemoAvailabilityAsync(long tutorDemoRequestId)
        {
            return await _repository .GetTutorDemoAvailabilityAsync(tutorDemoRequestId);
        }
        public async Task<bool> TutorConfirmDemoAsync(long tutorDemoRequestId, long tutorId)
        {
            return await _repository .TutorConfirmDemoAsync(tutorDemoRequestId,tutorId);
        }
        public async Task<bool> AdminConfirmParentDemoAsync( long tutorDemoRequestId, long updatedBy)
        {
            return await _repository .AdminConfirmParentDemoAsync( tutorDemoRequestId, updatedBy);
        }
        public async Task<bool> CompleteTutorDemoAsync( long tutorDemoRequestId,long updatedBy)
        {
            return await _repository.CompleteTutorDemoAsync(tutorDemoRequestId,  updatedBy);
        }
        public async Task<bool> SelectTutorAsync(
    long requestTutorId,
    long updatedBy)
        {
            return await _repository.SelectTutorAsync(
                requestTutorId,
                updatedBy
            );
        }
    }
}