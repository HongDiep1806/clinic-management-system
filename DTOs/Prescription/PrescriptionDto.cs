namespace ClinicManagementSystem.DTOs.Prescription
{
    public class PrescriptionDto
    {
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
    }

}
