namespace ClinicManagementSystem.DTOs.MedicalRecord
{
    public class CreateMedicalRecordDto
    {
        public int AppointmentId { get; set; }
        public string DiagnosisDescription { get; set; }
        public string Treatment { get; set; }
        public string Note { get; set; }
    }
}
