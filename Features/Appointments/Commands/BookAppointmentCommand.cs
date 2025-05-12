using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class BookAppointmentCommand : IRequest<BookAppointmentResponseDto>
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
