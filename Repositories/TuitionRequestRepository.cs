using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TutorNest.API.DTOs.TuitionRequest;
using TutorNest.API.Interfaces;
namespace TutorNest.API.Repositories
{
    public class TuitionRequestRepository:ITuitionRequestRepository
    {
        private readonly IDbConnection _connection;
        public TuitionRequestRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<CreateTuitionRequestResponseDto> CreateAsync(
           CreateTuitionRequestDto request)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@ParentId", request.ParentId);
            parameters.Add("@StudentId", request.StudentId);
            parameters.Add("@TuitionTypeId", request.TuitionTypeId);
            parameters.Add("@SubjectId", request.SubjectId);
            parameters.Add("@CityId", request.CityId);
            parameters.Add("@AreaId", request.AreaId);
            parameters.Add("@Budget", request.Budget);
            parameters.Add("@PreferredTutorGenderId",
                request.PreferredTutorGenderId);
            parameters.Add("@PreferredStartDate",
                request.PreferredStartDate);
            parameters.Add("@SpecialInstructions",
                request.SpecialInstructions);
            parameters.Add("@CreatedBy", request.CreatedBy);

            var result = await _connection.QuerySingleAsync<CreateTuitionRequestResponseDto>(
                "Tuition.usp_CreateTuitionRequest",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<TuitionRequestResponseDto?> GetByIdAsync(long tuitionRequestId)
        {
            var result = await _connection.QueryFirstOrDefaultAsync<TuitionRequestResponseDto>(
                "Tuition.usp_GetTuitionRequestById",
                new
                {
                    TuitionRequestId = tuitionRequestId
                },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<IEnumerable<TuitionRequestResponseDto>> GetAllAsync ()
        {
            var result = await _connection.QueryAsync<TuitionRequestResponseDto>(
                "Tuition.usp_GetAllTuitionRequests",
                commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<int> UpdateStatusAsync(
     long id,
     UpdateTuitionRequestStatusDto request)
        {
            var parameters = new
            {
                TuitionRequestId = id,
                request.StatusId,
                request.UpdatedBy
            };

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                "Tuition.usp_UpdateTuitionRequestStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<int> SendTutorRequestAsync(SendTutorRequestDto request)
        {
            var parameters = new
            {
                request.TuitionRequestId,
                request.TutorId,
                request.CreatedBy
            };

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                "Tuition.usp_SendTutorRequest",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<int> UpdateTutorRequestStatusAsync(
    long tutorRequestId,
    int requestStatusId,
    long updatedBy)
        {
            var parameters = new
            {
                TutorRequestId = tutorRequestId,
                RequestStatusId = requestStatusId,
                UpdatedBy = updatedBy
            };

            var result = await _connection.QueryFirstOrDefaultAsync<int>(
                "Tuition.usp_UpdateTutorRequestStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
        public async Task<IEnumerable<TutorRequestResponseDto>> GetTutorRequestsAsync()
        {
            

            return await _connection.QueryAsync<TutorRequestResponseDto>(
                "Tuition.usp_GetTutorRequests",
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<MyTuitionRequestResponseDto>> GetMyTuitionRequestsAsync(long parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ParentId", parentId);

            return await _connection.QueryAsync<MyTuitionRequestResponseDto>(
                "Tuition.usp_GetMyTuitionRequests",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
       
            public async Task<IEnumerable<TutorApplicationResponseDto>>
        GetTutorApplicationsByTuitionRequestAsync(
            long tuitionRequestId,
            long parentId)
            {
                var parameters = new DynamicParameters();

                parameters.Add(
                    "@TuitionRequestId",
                    tuitionRequestId);

                parameters.Add(
                    "@ParentId",
                    parentId);

                return await _connection.QueryAsync<TutorApplicationResponseDto>(
                    "Tuition.usp_GetTutorApplicationsByTuitionRequest",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            
        }
        
    }
}
