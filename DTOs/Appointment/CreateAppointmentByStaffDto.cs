namespace ClinicManagementSystem.DTOs.Appointment
{
    public class CreateAppointmentByStaffDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }

}
