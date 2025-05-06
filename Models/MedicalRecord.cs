namespace ClinicManagementSystem.Models
{
    public class MedicalRecord
    {
        public int MedicalRecordId { get; set; }
        public int AppointmentId { get; set; }
        public string DiagnosisDescription { get; set; }
        public string Treatment { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }

        public Appointment Appointment { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
