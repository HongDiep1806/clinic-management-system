using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class BookAppointmentCommand : IRequest<BookAppointmentResponseDto>
    {
        public BookAppointmentRequestDto RequestDto { get; set; }
        public BookAppointmentCommand(BookAppointmentRequestDto dto)
        {
            RequestDto = dto;
        }
    }
}


