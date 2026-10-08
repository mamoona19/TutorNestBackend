namespace TutorNest.API.DTOs.Payment
{
    public class PaymentVerificationResultDto
    {
        public long PaymentId { get; set; }
        public long InvoiceId { get; set; }
        public long TuitionContractId { get; set; }
        public long TuitionRequestId { get; set; }

        public decimal Amount { get; set; }

        public int PaymentStatusId { get; set; }
        public string PaymentStatusName { get; set; } = string.Empty;

        public int InvoiceStatusId { get; set; }
        public string InvoiceStatusName { get; set; } = string.Empty;

        public int ContractStatusId { get; set; }
        public string ContractStatusName { get; set; } = string.Empty;

        public int TuitionRequestStatusId { get; set; }
        public string TuitionRequestStatusName { get; set; } = string.Empty;

        public bool IsFirstPaymentReceived { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}