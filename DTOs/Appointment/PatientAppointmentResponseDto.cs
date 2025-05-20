namespace ClinicManagementSystem.DTOs.Appointment
{
    public class PatientAppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
    }
}
