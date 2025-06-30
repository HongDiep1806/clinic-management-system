namespace ClinicManagementSystem.DTOs.Appointment
{
    public class AppointmentDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public string Status { get; set; }
        public string Date => AppointmentDate.ToString("yyyy-MM-dd");
        public string Time => AppointmentDate.ToString("HH:mm");
    }
}
