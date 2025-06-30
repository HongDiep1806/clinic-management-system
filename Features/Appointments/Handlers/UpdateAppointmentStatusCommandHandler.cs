using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, bool>
    {
        private readonly IAppointmentService _appointmentService;
        public UpdateAppointmentStatusCommandHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<bool> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentById(request.RequestDto.AppointmentId);
            if (!Enum.TryParse<AppointmentStatus>(request.RequestDto.Status, true, out var statusEnum))
            {
                throw new ArgumentException("Invalid status");

            }
            appointment.Status = statusEnum;
            return await _appointmentService.UpdateAppointment(appointment, request.RequestDto.AppointmentId);

        }
    }
}
