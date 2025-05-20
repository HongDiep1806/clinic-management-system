namespace ClinicManagementSystem.DTOs.Prescription
{
    public class CreatePrescriptionDto
    {
        public int RecordId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }

}
