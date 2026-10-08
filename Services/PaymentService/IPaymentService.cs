using TutorNest.API.DTOs.Payment;

namespace TutorNest.API.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentVerificationResultDto?> VerifyPaymentAsync(
            long paymentId,
            long verifiedBy);
        Task<IEnumerable<ParentInvoiceDto>> GetParentInvoicesAsync(
            long parentId);
        Task<PaymentCreatedResultDto?> CreatePaymentAsync(
    long invoiceId,
    long parentId,
    int paymentMethodId,
    int paymentGatewayId,
    string? transactionReference);
        Task<IEnumerable<PaymentAccountDto>>
    GetPaymentAccountsAsync();

        Task<PaymentAccountDto?>
            SavePaymentAccountAsync(
                SavePaymentAccountDto request,
                long createdBy);

        Task<PaymentAccountDto?>
            UpdatePaymentAccountAsync(
                long paymentAccountId,
                SavePaymentAccountDto request,
                long updatedBy);

        Task<PaymentAccountDto?>
            UpdatePaymentAccountStatusAsync(
                long paymentAccountId,
                bool isActive,
                long updatedBy);
    }
}