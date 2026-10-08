using TutorNest.API.DTOs.Admin;
using TutorNest.API.DTOs.MyTutorApplication;
using TutorNest.API.DTOs.PendingTutorDemoRequest;
using TutorNest.API.DTOs.Tutor;

namespace TutorNest.API.Repositories.TutorRequest
{
    public interface ITutorRequestRepository
    {
        Task<IEnumerable<TutorRequestDto>> GetTutorRequestsAsync(long userId);
        Task<string> AcceptTutorRequestAsync( long requestTutorId,long tutorId, int requestStatusId, string? tutorRemarks,long updatedBy);

        Task<string> RejectTutorRequestAsync( long requestTutorId,long tutorId,int requestStatusId,string? tutorRemarks,long updatedBy);
        Task<long?> GetTutorIdByUserIdAsync(long userId);
        Task<IEnumerable<AvailableTuitionRequestDto>> GetAvailableTuitionRequestsAsync(long tutorId);
        Task<long> ApplyForTuitionRequestAsync( long tuitionRequestId, long tutorId, string? tutorRemarks, long createdBy);
        Task<IEnumerable<MyTutorApplicationDto>> GetMyTutorApplicationsAsync( long tutorId);
        Task<IEnumerable<AdminTutorApplicationDto>> GetTutorApplicationsForAdminAsync();
        Task<string> AdminUpdateTutorApplicationAsync(  long requestTutorId, int requestStatusId, string? adminRemarks, long updatedBy);
        Task<IEnumerable<ParentTutorApplicationDto>> GetAcceptedTutorApplicationsForParentAsync(long parentId);
        Task<bool> CreateTutorDemoRequestAsync( TutorDemoRequestDto request);
        Task<IEnumerable<PendingTutorDemoRequestDto>> GetPendingTutorDemoRequestsAsync();
        Task<long?> AddTutorDemoAvailabilityAsync( TutorDemoAvailabilityDto request);
        Task<IEnumerable<TutorDemoRequestListDto>> GetTutorDemoRequestsAsync(long tutorId);
        Task<bool> ScheduleTutorDemoAsync(TutorDemoScheduleDto  request);
        Task<IEnumerable<TutorDemoAvailabilityListDto>> GetTutorDemoAvailabilityAsync(long tutorDemoRequestId);
        Task<bool> TutorConfirmDemoAsync( long tutorDemoRequestId, long tutorId);
        Task<bool> AdminConfirmParentDemoAsync(long tutorDemoRequestId, long updatedBy);
        Task<bool> CompleteTutorDemoAsync( long tutorDemoRequestId, long updatedBy);
        Task<bool> SelectTutorAsync( long requestTutorId,long updatedBy);
    }
    



    }
