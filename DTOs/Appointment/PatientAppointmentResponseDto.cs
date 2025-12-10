namespace ClinicManagementSystem.DTOs.Appointment
{
    public class PatientAppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public string DoctorName { get; set; }
        public string Status { get; set; }

        public string Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
