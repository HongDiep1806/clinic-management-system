using ClinicManagementSystem.DTOs.Appointment;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class UpdateAppointmentStatusCommand : IRequest<bool>
    {
      public UpdateAppointmentStatusRequestDto RequestDto { get; set; }
        public UpdateAppointmentStatusCommand(UpdateAppointmentStatusRequestDto dto)
        {
            RequestDto = dto;
        }
    }
}
