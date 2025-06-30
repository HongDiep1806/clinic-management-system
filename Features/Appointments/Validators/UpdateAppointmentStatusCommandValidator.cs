using ClinicManagementSystem.Features.Appointments.Commands;
using ClinicManagementSystem.Models;
using FluentValidation;

namespace ClinicManagementSystem.Features.Appointments.Validators
{
    public class UpdateAppointmentStatusCommandValidator : AbstractValidator<UpdateAppointmentStatusCommand>
    {
        public UpdateAppointmentStatusCommandValidator()
        {
            RuleFor(x => x.RequestDto.Status)
                .NotEmpty()
                .Must(BeAValidStatus)
                .WithMessage("Status must be one of: Pending, Confirmed, Cancelled, Completed");
        }

        private bool BeAValidStatus(string status)
        {
            return Enum.TryParse(typeof(AppointmentStatus), status, true, out _);
        }
    }
}
