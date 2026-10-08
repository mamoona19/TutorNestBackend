namespace TutorNest.API.DTOs.Payment
{
    public class SavePaymentAccountDto
    {
        public int PaymentMethodId { get; set; }

        public int? PaymentGatewayId { get; set; }

        public string AccountName { get; set; } = string.Empty;

        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IBAN { get; set; }

        public string? MobileNumber { get; set; }

        public string? Instructions { get; set; }
    }
}
