using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class CancelAppointmentByStaffCommandHandler : IRequestHandler<CancelAppointmentByStaffCommand, bool>
    {
        private readonly IAppointmentService _appointmentService;

        public CancelAppointmentByStaffCommandHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<bool> Handle(CancelAppointmentByStaffCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentById(request.AppointmentId);

            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found.");

            if (appointment.Status == AppointmentStatus.Completed ||
                appointment.Status == AppointmentStatus.Cancelled)
                throw new InvalidOperationException("Appointment cannot be cancelled.");

            appointment.Status = AppointmentStatus.Cancelled;
            return await _appointmentService.UpdateAppointment(appointment, request.AppointmentId);
        }
    }
}
