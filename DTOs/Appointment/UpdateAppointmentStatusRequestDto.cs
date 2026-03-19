using ClinicManagementSystem.Models;
using MediatR;

namespace ClinicManagementSystem.DTOs.Appointment
{
    public class UpdateAppointmentStatusRequestDto
    {
        public int AppointmentId { get; set; }

        public string Status { get; set; }

        public string? Reason { get; set; }
    }
}
