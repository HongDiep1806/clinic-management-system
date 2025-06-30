using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Commands
{
    public class CreateAppointmentByStaffCommand : IRequest<AppointmentDto>
    {
        public CreateAppointmentByStaffDto Dto { get; set; }

        public CreateAppointmentByStaffCommand(CreateAppointmentByStaffDto dto)
        {
            Dto = dto;
        }
    }
}
