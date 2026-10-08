using Dapper;
using System.Data;
using TutorNest.API.DTOs.Payment;


namespace TutorNest.API.Repositories.Payment
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IDbConnection _connection;

        public PaymentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<PaymentVerificationResultDto?> VerifyPaymentAsync(
            long paymentId,
            long verifiedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@PaymentId",
                paymentId,
                DbType.Int64
            );

            parameters.Add(
                "@VerifiedBy",
                verifiedBy,
                DbType.Int64
            );

            return await _connection.QueryFirstOrDefaultAsync<PaymentVerificationResultDto>(
                "Payment.usp_VerifyPayment",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<ParentInvoiceDto>> GetParentInvoicesAsync(
    long parentId)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@ParentId",
                parentId,
                DbType.Int64
            );

            return await _connection.QueryAsync<ParentInvoiceDto>(
                "Payment.usp_GetParentInvoices",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<PaymentCreatedResultDto?> CreatePaymentAsync(
    long invoiceId,
    long parentId,
    int paymentMethodId,
    int paymentGatewayId,
    string? transactionReference)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@InvoiceId",
                invoiceId,
                DbType.Int64
            );

            parameters.Add(
                "@ParentId",
                parentId,
                DbType.Int64
            );

            parameters.Add(
                "@PaymentMethodId",
                paymentMethodId,
                DbType.Int32
            );

            parameters.Add(
                "@PaymentGatewayId",
                paymentGatewayId,
                DbType.Int32
            );

            parameters.Add(
                "@TransactionReference",
                transactionReference,
                DbType.String
            );

            return await _connection.QueryFirstOrDefaultAsync<PaymentCreatedResultDto>(
                "Payment.usp_CreatePayment",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<IEnumerable<PaymentAccountDto>>
    GetPaymentAccountsAsync()
        {
            return await _connection.QueryAsync<PaymentAccountDto>(
                "Payment.usp_GetPaymentAccounts",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<PaymentAccountDto?>
    SavePaymentAccountAsync(
        SavePaymentAccountDto request,
        long createdBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@PaymentMethodId",
                request.PaymentMethodId,
                DbType.Int32
            );

            parameters.Add(
                "@PaymentGatewayId",
                request.PaymentGatewayId,
                DbType.Int32
            );

            parameters.Add(
                "@AccountName",
                request.AccountName,
                DbType.String
            );

            parameters.Add(
                "@BankName",
                request.BankName,
                DbType.String
            );

            parameters.Add(
                "@AccountNumber",
                request.AccountNumber,
                DbType.String
            );

            parameters.Add(
                "@IBAN",
                request.IBAN,
                DbType.String
            );

            parameters.Add(
                "@MobileNumber",
                request.MobileNumber,
                DbType.String
            );

            parameters.Add(
                "@Instructions",
                request.Instructions,
                DbType.String
            );

            parameters.Add(
                "@CreatedBy",
                createdBy,
                DbType.Int64
            );

            return await _connection.QueryFirstOrDefaultAsync<PaymentAccountDto>(
                "Payment.usp_SavePaymentAccount",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<PaymentAccountDto?>
    UpdatePaymentAccountAsync(
        long paymentAccountId,
        SavePaymentAccountDto request,
        long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@PaymentAccountId",
                paymentAccountId,
                DbType.Int64
            );

            parameters.Add(
                "@PaymentMethodId",
                request.PaymentMethodId,
                DbType.Int32
            );

            parameters.Add(
                "@PaymentGatewayId",
                request.PaymentGatewayId,
                DbType.Int32
            );

            parameters.Add(
                "@AccountName",
                request.AccountName,
                DbType.String
            );

            parameters.Add(
                "@BankName",
                request.BankName,
                DbType.String
            );

            parameters.Add(
                "@AccountNumber",
                request.AccountNumber,
                DbType.String
            );

            parameters.Add(
                "@IBAN",
                request.IBAN,
                DbType.String
            );

            parameters.Add(
                "@MobileNumber",
                request.MobileNumber,
                DbType.String
            );

            parameters.Add(
                "@Instructions",
                request.Instructions,
                DbType.String
            );

            parameters.Add(
                "@UpdatedBy",
                updatedBy,
                DbType.Int64
            );

            return await _connection.QueryFirstOrDefaultAsync<PaymentAccountDto>(
                "Payment.usp_UpdatePaymentAccount",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<PaymentAccountDto?>
    UpdatePaymentAccountStatusAsync(
        long paymentAccountId,
        bool isActive,
        long updatedBy)
        {
            var parameters = new DynamicParameters();

            parameters.Add(
                "@PaymentAccountId",
                paymentAccountId,
                DbType.Int64
            );

            parameters.Add(
                "@IsActive",
                isActive,
                DbType.Boolean
            );

            parameters.Add(
                "@UpdatedBy",
                updatedBy,
                DbType.Int64
            );

            return await _connection.QueryFirstOrDefaultAsync<PaymentAccountDto>(
                "Payment.usp_UpdatePaymentAccountStatus",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}