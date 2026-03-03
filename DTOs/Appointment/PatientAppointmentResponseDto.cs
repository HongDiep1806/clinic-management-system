namespace ClinicManagementSystem.DTOs.Appointment
{
    public class PatientAppointmentResponseDto
    {
        //public int AppointmentId { get; set; }

        //public string DoctorName { get; set; }
        //public string Status { get; set; }

        //public string Date { get; set; }
        //public DateTime CreatedAt { get; set; }
        public int AppointmentId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        public string Date { get; set; }
        public string Status { get; set; }
        public int DoctorId { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public DateTime CreatedAt { get; set; }   // Queue ordering
    }
}
