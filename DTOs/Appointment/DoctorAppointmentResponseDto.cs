﻿namespace ClinicManagementSystem.DTOs.Appointment
{
    public class DoctorAppointmentResponseDto
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; }
        public string Date { get; set; } 
        public string Time { get; set; }

    }
}
