using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using System.Data;
using System.Security.Claims;

namespace ClinicManagementSystem.Features.Appointments.Handlers
{
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand, bool>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateAppointmentStatusCommandHandler(IAppointmentService appointmentService, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetByIdWithIncludes(request.RequestDto.AppointmentId);
            if (!Enum.TryParse<AppointmentStatus>(request.RequestDto.Status, true, out var statusEnum))
            {
                throw new ArgumentException("Invalid status");

            }
            appointment.Status = statusEnum;

            if (statusEnum == AppointmentStatus.Cancelled)
            {
                var role = _httpContextAccessor.HttpContext.User
       .FindFirst(ClaimTypes.Role)?.Value;
                if (role == "Patient")
                {
                    appointment.Reason = "Self Cancel";
                }
                else
                {
                    appointment.Reason = request.RequestDto.Reason;
                }

                await _emailService.SendEmailAsync(
                    appointment.Patient.Email,
                    "Appointment Cancelled",
                    $"Your appointment on {appointment.Date} has been cancelled.\nReason: {appointment.Reason}"
                );
            }
            return await _appointmentService.UpdateAppointment(appointment, request.RequestDto.AppointmentId);

        }
    }
}
