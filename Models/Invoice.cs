namespace ClinicManagementSystem.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public int RecordId { get; set; }
        public float TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }

        public MedicalRecord Record { get; set; }
    }
}
