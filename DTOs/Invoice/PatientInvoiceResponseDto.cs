namespace ClinicManagementSystem.DTOs.Invoice
{
    public class PatientInvoiceResponseDto
    {
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }

}
