namespace ClinicManagementSystem.DTOs.Appointment
{
    public class BookAppointmentRequestDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Date { get; set; }

        public string? Reason { get; set; }
    }

}

