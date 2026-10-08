namespace TutorNest.API.DTOs.Payment
{
    public class ParentInvoiceDto
    {
        public long InvoiceId { get; set; }

        public long TuitionContractId { get; set; }

        public long TuitionRequestId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal InvoiceAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal NetAmount { get; set; }

        public int InvoiceStatusId { get; set; }

        public string InvoiceStatusName { get; set; } = string.Empty;

        public decimal MonthlyFee { get; set; }

        public string PaymentTermsName { get; set; } = string.Empty;

        public string TutorName { get; set; } = string.Empty;

        public string StudentName { get; set; } = string.Empty;
    }
}