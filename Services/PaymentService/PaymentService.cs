using TutorNest.API.DTOs.Payment;
using TutorNest.API.Interfaces;
using TutorNest.API.Repositories.Payment;

namespace TutorNest.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        // ==========================================
        // ADMIN - VERIFY PAYMENT
        // ==========================================

        public async Task<PaymentVerificationResultDto?> VerifyPaymentAsync(
            long paymentId,
            long verifiedBy)
        {
            return await _repository.VerifyPaymentAsync(
                paymentId,
                verifiedBy
            );
        }


        // ==========================================
        // PARENT - GET MY INVOICES
        // ==========================================

        public async Task<IEnumerable<ParentInvoiceDto>> GetParentInvoicesAsync(
            long parentId)
        {
            return await _repository.GetParentInvoicesAsync(
                parentId
            );
        }


        // ==========================================
        // PARENT - CREATE PAYMENT
        // ==========================================

        public async Task<PaymentCreatedResultDto?> CreatePaymentAsync(
            long invoiceId,
            long parentId,
            int paymentMethodId,
            int paymentGatewayId,
            string? transactionReference)
        {
            return await _repository.CreatePaymentAsync(
                invoiceId,
                parentId,
                paymentMethodId,
                paymentGatewayId,
                transactionReference
            );
        }
        // ==========================================
        // ADMIN - PAYMENT ACCOUNTS
        // ==========================================

        public async Task<IEnumerable<PaymentAccountDto>>
            GetPaymentAccountsAsync()
        {
            return await _repository.GetPaymentAccountsAsync();
        }


        public async Task<PaymentAccountDto?>
            SavePaymentAccountAsync(
                SavePaymentAccountDto request,
                long createdBy)
        {
            return await _repository.SavePaymentAccountAsync(
                request,
                createdBy
            );
        }


        public async Task<PaymentAccountDto?>
            UpdatePaymentAccountAsync(
                long paymentAccountId,
                SavePaymentAccountDto request,
                long updatedBy)
        {
            return await _repository.UpdatePaymentAccountAsync(
                paymentAccountId,
                request,
                updatedBy
            );
        }


        public async Task<PaymentAccountDto?>
            UpdatePaymentAccountStatusAsync(
                long paymentAccountId,
                bool isActive,
                long updatedBy)
        {
            return await _repository.UpdatePaymentAccountStatusAsync(
                paymentAccountId,
                isActive,
                updatedBy
            );
        }
    }
}