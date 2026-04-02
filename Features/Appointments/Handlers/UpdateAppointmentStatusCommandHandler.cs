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

        public UpdateAppointmentStatusCommandHandler(
            IAppointmentService appointmentService,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor)
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
                var role = _httpContextAccessor.HttpContext?.User
                    ?.FindFirst(ClaimTypes.Role)?.Value;

                if (role == "Patient")
                {
                    appointment.Reason = "Self Cancel";
                }
                else
                {
                    appointment.Reason = request.RequestDto.Reason;
                }

                var emailBody = BuildCancelledAppointmentEmailBody(
                    appointment.Patient.FullName ?? "Patient",
                    appointment.Date,
                    appointment.Reason ?? "No reason provided"
                );

                await _emailService.SendEmailAsync(
                    appointment.Patient.Email,
                    "Appointment Cancelled",
                    emailBody
                );
            }

            return await _appointmentService.UpdateAppointment(appointment, request.RequestDto.AppointmentId);
        }

        private string BuildCancelledAppointmentEmailBody(string patientName, DateTime appointmentDate, string reason)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Appointment Cancelled</title>
</head>
<body style='margin:0; padding:0; background-color:#f4f7fb; font-family:Arial, Helvetica, sans-serif; color:#333333;'>
    <table role='presentation' width='100%' cellspacing='0' cellpadding='0' style='background-color:#f4f7fb; margin:0; padding:30px 0;'>
        <tr>
            <td align='center'>
                <table role='presentation' width='600' cellspacing='0' cellpadding='0' 
                       style='background-color:#ffffff; border-radius:12px; overflow:hidden; box-shadow:0 4px 20px rgba(0,0,0,0.08);'>

                    <tr>
                        <td style='background:linear-gradient(135deg, #2563eb, #1e40af); padding:28px 32px; text-align:center;'>
                            <h1 style='margin:0; color:#ffffff; font-size:24px;'>Clinic Management System</h1>
                            <p style='margin:8px 0 0; color:#dbeafe; font-size:14px;'>
                                Appointment Notification
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td style='padding:32px;'>
                            <h2 style='margin:0 0 16px; font-size:22px; color:#dc2626;'>
                                Appointment Cancelled
                            </h2>

                            <p style='margin:0 0 16px; font-size:15px; line-height:1.7;'>
                                Dear <strong>{patientName}</strong>,
                            </p>

                            <p style='margin:0 0 24px; font-size:15px; line-height:1.7;'>
                                We would like to inform you that your appointment has been 
                                <strong style='color:#dc2626;'>cancelled</strong>.
                            </p>

                            <table role='presentation' width='100%' cellspacing='0' cellpadding='0'
                                   style='border:1px solid #e5e7eb; border-radius:10px; background-color:#f9fafb; margin-bottom:24px;'>
                                <tr>
                                    <td style='padding:18px 20px;'>
                                        <p style='margin:0 0 10px; font-size:14px;'>
                                            <strong>Appointment Date:</strong> {appointmentDate:dddd, dd MMMM yyyy}
                                        </p>
                                        <p style='margin:0 0 10px; font-size:14px;'>
                                            <strong>Appointment Time:</strong> {appointmentDate:hh:mm tt}
                                        </p>
                                        <p style='margin:0; font-size:14px;'>
                                            <strong>Reason:</strong> {reason}
                                        </p>
                                    </td>
                                </tr>
                            </table>

                            <p style='margin:0 0 24px; font-size:15px; line-height:1.7;'>
                                Please contact the clinic or log in to the system to book another appointment if needed.
                            </p>
                            <p style='margin:0; font-size:14px; color:#6b7280; line-height:1.7;'>
                                Thank you,<br/>
                                <strong>Clinic Management System</strong>
                            </p>
                        </td>
                    </tr>

                    <tr>
                        <td style='background-color:#f3f4f6; padding:18px 32px; text-align:center;'>
                            <p style='margin:0; font-size:12px; color:#6b7280;'>
                                This is an automated email. Please do not reply directly to this message.
                            </p>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}