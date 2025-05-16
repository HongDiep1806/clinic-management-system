namespace ClinicManagementSystem.DTOs.Appointment
{
    public class BookAppointmentRequestDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
