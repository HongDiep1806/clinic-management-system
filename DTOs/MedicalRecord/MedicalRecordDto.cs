using ClinicManagementSystem.DTOs.Prescription;

namespace ClinicManagementSystem.DTOs.MedicalRecord
{
    public class MedicalRecordDto
    {
        public int MedicalRecordId { get; set; }
        public int AppointmentId { get; set; }
        public string DiagnosisDescription { get; set; }
        public string Treatment { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; }

    }
}
