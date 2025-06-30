namespace ClinicManagementSystem.DTOs.Invoice
{
    public class InvoiceDto
    {
        public int InvoiceId { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }

}
