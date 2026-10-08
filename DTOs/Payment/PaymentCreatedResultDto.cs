namespace TutorNest.API.DTOs.Payment
{
    public class PaymentCreatedResultDto
    {
        public long PaymentId { get; set; }

        public long InvoiceId { get; set; }

        public long TuitionContractId { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCode { get; set; } = string.Empty;

        public int PaymentStatusId { get; set; }

        public string PaymentStatusName { get; set; } = string.Empty;

        public int PaymentMethodId { get; set; }

        public string PaymentMethodName { get; set; } = string.Empty;

        public int PaymentGatewayId { get; set; }

        public string PaymentGatewayName { get; set; } = string.Empty;

        public string? TransactionReference { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}