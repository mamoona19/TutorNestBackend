namespace TutorNest.API.DTOs.Payment
{
    public class CreatePaymentDto
    {
        public long InvoiceId { get; set; }

        public int PaymentMethodId { get; set; }

        public int PaymentGatewayId { get; set; }

        public string? TransactionReference { get; set; }
    }
}