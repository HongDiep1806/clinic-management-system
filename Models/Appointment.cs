using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementSystem.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }

        public DateTime Date { get; set; }
        public string? Reason { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        [ForeignKey("PatientId")]
        public User? Patient { get; set; }   // 🔥 nullable

        [ForeignKey("DoctorId")]
        public User? Doctor { get; set; }    // 🔥 nullable
        public int DepartmentId { get; set; }         // NEW SNAPSHOT
        public string? DepartmentName { get; set; }   // 
    }

}
