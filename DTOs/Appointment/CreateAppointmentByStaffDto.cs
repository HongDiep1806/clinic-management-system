namespace ClinicManagementSystem.DTOs.Appointment
{
    public class CreateAppointmentByStaffDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime Date { get; set; }

        // Optional lý do — staff có thể nhập hoặc không
        public string? Reason { get; set; }
    }
}
