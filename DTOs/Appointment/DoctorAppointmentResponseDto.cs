namespace ClinicManagementSystem.DTOs.Appointment
{
    public class DoctorAppointmentResponseDto
    {
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }
        public string Status { get; set; }

        public string Date { get; set; }
        public DateTime CreatedAt { get; set; }   // Doctor thấy ai đặt trước
    }
}
