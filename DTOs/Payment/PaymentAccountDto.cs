namespace TutorNest.API.DTOs.Payment
{
    public class PaymentAccountDto
    {
        public long PaymentAccountId { get; set; }

        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; } = string.Empty;

        public int? PaymentGatewayId { get; set; }
        public string? PaymentGatewayName { get; set; }

        public string AccountName { get; set; } = string.Empty;

        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IBAN { get; set; }

        public string? MobileNumber { get; set; }

        public string? Instructions { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
    }
}