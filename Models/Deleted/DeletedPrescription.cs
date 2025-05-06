using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Models.Deleted
{
    public class DeletedPrescription
    {
        [Key]
        public int DeletedPrescriptionId { get; set; }
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }
}
