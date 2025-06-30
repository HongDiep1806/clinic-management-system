using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public AppointmentStatus Status { get; set; }
        [ForeignKey("PatientId")]
        [InverseProperty("AppointmentsAsPatient")]
        public User Patient { get; set; }
        [ForeignKey("DoctorId")]
        [InverseProperty("AppointmentsAsDoctor")]
        public User Doctor { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
    }
}
