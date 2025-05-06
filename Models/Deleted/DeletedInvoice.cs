using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedInvoice
    {
        [Key]
        public int DeletedInvoiceId { get; set; }
        public int InvoiceId { get; set; }
        public int RecordId { get; set; }
        public float TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
}
