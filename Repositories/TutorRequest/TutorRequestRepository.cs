using Dapper;
using System.Data;
using System.Data.Common;
using TutorNest.API.DTOs.Admin;
using TutorNest.API.DTOs.MyTutorApplication;
using TutorNest.API.DTOs.PendingTutorDemoRequest;
using TutorNest.API.DTOs.Tutor;
using TutorNest.API.DTOs.Tutor;
using TutorNest.API.Interfaces;
using TutorNest.API.Repositories.TutorRequest;

namespace TutorNest.API.Repositories
{
    public class TutorRequestRepository : ITutorRequestRepository
    {
        private readonly IDbConnection _db;

        public TutorRequestRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<IEnumerable<TutorRequestDto>> GetTutorRequestsAsync(long userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId);

            return await _db.QueryAsync<TutorRequestDto>(
                "Tuition.usp_GetTutorRequests",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<string> AcceptTutorRequestAsync(
    long requestTutorId,
    long tutorId,
    int requestStatusId,
    string? tutorRemarks,
    long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RequestTutorId", requestTutorId);
            parameters.Add("@TutorId", tutorId);
            parameters.Add("@RequestStatusId", requestStatusId);
            parameters.Add("@TutorRemarks", tutorRemarks);
            parameters.Add("@UpdatedBy", updatedBy);

            return await _db.QuerySingleAsync<string>(
                "Tuition.usp_AcceptTutorRequest",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<string> RejectTutorRequestAsync(
            long requestTutorId,
            long tutorId,
            int requestStatusId,
            string? tutorRemarks,
            long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RequestTutorId", requestTutorId);
            parameters.Add("@TutorId", tutorId);
            parameters.Add("@RequestStatusId", requestStatusId);
            parameters.Add("@TutorRemarks", tutorRemarks);
            parameters.Add("@UpdatedBy", updatedBy);

            return await _db.QuerySingleAsync<string>(
                "Tuition.usp_RejectTutorRequest",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<long?> GetTutorIdByUserIdAsync(long userId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@UserId", userId);

            return await _db.QuerySingleOrDefaultAsync<long?>(
                "Tutor.usp_GetTutorIdByUserId",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<AvailableTuitionRequestDto>> GetAvailableTuitionRequestsAsync(long tutorId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TutorId", tutorId);

            return await _db.QueryAsync<AvailableTuitionRequestDto>(
                "Tuition.usp_GetAvailableTuitionRequests",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<long> ApplyForTuitionRequestAsync(
    long tuitionRequestId,
    long tutorId,
    string? tutorRemarks,
    long createdBy)
        {
            

            var parameters = new DynamicParameters();

            parameters.Add("@TuitionRequestId", tuitionRequestId);
            parameters.Add("@TutorId", tutorId);
            parameters.Add("@TutorRemarks", tutorRemarks);
            parameters.Add("@CreatedBy", createdBy);

            return await _db.ExecuteScalarAsync<long>(
                "Tuition.usp_ApplyForTuitionRequest",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<MyTutorApplicationDto>> GetMyTutorApplicationsAsync(
    long tutorId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TutorId", tutorId);

            return await _db.QueryAsync<MyTutorApplicationDto>(
                "Tuition.usp_GetMyTutorApplications",
                parameters,
                commandType: CommandType.StoredProcedure
            );

        }
        public async Task<IEnumerable<AdminTutorApplicationDto>>
    GetTutorApplicationsForAdminAsync()
        {
            return await _db.QueryAsync<AdminTutorApplicationDto>(
                "Tuition.usp_GetTutorApplicationsForAdmin",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<string> AdminUpdateTutorApplicationAsync(
    long requestTutorId,
    int requestStatusId,
    string? adminRemarks,
    long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RequestTutorId", requestTutorId);
            parameters.Add("@RequestStatusId", requestStatusId);
            parameters.Add("@AdminRemarks", adminRemarks);
            parameters.Add("@UpdatedBy", updatedBy);

            return await _db.QuerySingleAsync<string>(
                "Tuition.usp_AdminUpdateTutorApplication",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<ParentTutorApplicationDto>>
    GetAcceptedTutorApplicationsForParentAsync(long parentId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ParentId", parentId);

            return await _db.QueryAsync<ParentTutorApplicationDto>(
                "Tuition.usp_GetAcceptedTutorApplicationsForParent",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<bool> CreateTutorDemoRequestAsync(
    TutorDemoRequestDto request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TuitionRequestId", request.TuitionRequestId);
            parameters.Add("@RequestTutorId", request.RequestTutorId);
            parameters.Add("@ParentId", request.ParentId);
            parameters.Add("@TutorId", request.TutorId);
            parameters.Add("@ParentRemarks", request.ParentRemarks);

            var result = await _db.ExecuteScalarAsync<int>(
                "Tuition.usp_CreateTutorDemoRequest",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result > 0;
        }
        public async Task<IEnumerable<PendingTutorDemoRequestDto>>
    GetPendingTutorDemoRequestsAsync()
        {
            return await _db.QueryAsync<PendingTutorDemoRequestDto>(
                "Tuition.usp_GetPendingTutorDemoRequests",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<long?> AddTutorDemoAvailabilityAsync(
    TutorDemoAvailabilityDto request)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@TutorDemoRequestId",
                request.TutorDemoRequestId
            );

            parameters.Add(
                "@TutorId",
                request.TutorId
            );

            parameters.Add(
                "@AvailableDate",
                request.AvailableDate
            );

            parameters.Add(
                "@StartTime",
                request.StartTime
            );

            parameters.Add(
                "@EndTime",
                request.EndTime
            );

            parameters.Add(
                "@TutorRemarks",
                request.TutorRemarks
            );

            parameters.Add(
                "@CreatedBy",
                request.CreatedBy
            );

            return await _db.ExecuteScalarAsync<long?>(
                "Tuition.usp_AddTutorDemoAvailability",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<TutorDemoRequestListDto>>  GetTutorDemoRequestsAsync(long tutorId)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@TutorId",
                tutorId
            );

            return await _db.QueryAsync<TutorDemoRequestListDto>(
                "Tuition.usp_GetTutorDemoRequests",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<bool> ScheduleTutorDemoAsync(
    TutorDemoScheduleDto request)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@TutorDemoRequestId",
                request.TutorDemoRequestId
            );

            parameters.Add(
                "@AvailabilityId",
                request.AvailabilityId
            );

            parameters.Add(
                "@AdminRemarks",
                request.AdminRemarks
            );

            parameters.Add(
                "@UpdatedBy",
                request.UpdatedBy
            );

            var result = await _db.QuerySingleAsync<dynamic>(
                "Tuition.usp_ScheduleTutorDemo",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.Success;
        }
        public async Task<IEnumerable<TutorDemoAvailabilityListDto>>
    GetTutorDemoAvailabilityAsync(long tutorDemoRequestId)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@TutorDemoRequestId",
                tutorDemoRequestId
            );

            return await _db.QueryAsync<TutorDemoAvailabilityListDto>(
                "Tuition.usp_GetTutorDemoAvailability",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<bool> TutorConfirmDemoAsync(
    long tutorDemoRequestId,
    long tutorId)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TutorDemoRequestId", tutorDemoRequestId);
            parameters.Add("@TutorId", tutorId);

            var result = await _db.QuerySingleAsync<dynamic>(
                "Tuition.usp_TutorConfirmDemo",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.Success;
        }
        public async Task<bool> AdminConfirmParentDemoAsync(
    long tutorDemoRequestId,
    long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TutorDemoRequestId", tutorDemoRequestId);
            parameters.Add("@UpdatedBy", updatedBy);

            var result = await _db.QuerySingleAsync<dynamic>(
                "Tuition.usp_AdminConfirmParentDemo",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.Success;
        }
        public async Task<bool> CompleteTutorDemoAsync(long tutorDemoRequestId, long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@TutorDemoRequestId",
                tutorDemoRequestId
            );

            parameters.Add(
                "@UpdatedBy",
                updatedBy
            );

            var result = await _db.QuerySingleAsync<dynamic>(
                "Tuition.usp_CompleteTutorDemo",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.Success;
        }
        public async Task<bool> SelectTutorAsync(
    long requestTutorId,
    long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@RequestTutorId",
                requestTutorId
            );

            parameters.Add(
                "@UpdatedBy",
                updatedBy
            );

            var result = await _db.QuerySingleAsync<dynamic>(
                "Tuition.usp_SelectTutor",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result.Success;
        }
    }
}