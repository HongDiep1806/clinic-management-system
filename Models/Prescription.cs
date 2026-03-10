using System.Text.Json.Serialization;

namespace ClinicManagementSystem.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public int MedicineId { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
        [JsonIgnore]

        public MedicalRecord Record { get; set; }
        public Medicine Medicine { get; set; }
    }
}
