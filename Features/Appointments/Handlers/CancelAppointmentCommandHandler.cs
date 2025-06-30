using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, bool>
    {
        private readonly IAppointmentService _appointmentService;

        public CancelAppointmentCommandHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentById(request.AppointmentId);
            if (appointment == null || appointment.PatientId != request.PatientId)
                throw new UnauthorizedAccessException("You are not allowed to cancel this appointment.");

            if (appointment.Status == AppointmentStatus.Completed || appointment.Status == AppointmentStatus.Cancelled || appointment.Status==AppointmentStatus.Confirmed)
                throw new InvalidOperationException("Appointment cannot be cancelled at this stage.");
            appointment.Status = AppointmentStatus.Cancelled;
            return await _appointmentService.UpdateAppointment(appointment, request.AppointmentId);
        }
    }

}
