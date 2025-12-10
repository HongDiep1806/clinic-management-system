namespace ClinicManagementSystem.DTOs.Appointment
{
    public class BookAppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public string Status { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Date { get; set; } 
        public string Time { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
