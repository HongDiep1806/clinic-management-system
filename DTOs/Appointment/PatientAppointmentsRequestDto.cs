namespace ClinicManagementSystem.DTOs.Appointment
{
    public class PatientAppointmentsRequestDto
    {
        public int PatientId { get; set; } 
        public DateTime? Date { get; set; }
    }

}
