using ClinicManagementSystem.Features.Appointments.Commands;
using FluentValidation;

namespace ClinicManagementSystem.Features.Appointments.Validators
{
    public class BookAppointmentCommandValidator : AbstractValidator<BookAppointmentCommand>
    {
        public BookAppointmentCommandValidator()
        {
            RuleFor(x => x.RequestDto.PatientId)
            .GreaterThan(0).WithMessage("PatientId must be greater than 0");

            RuleFor(x => x.RequestDto.DoctorId)
                .GreaterThan(0).WithMessage("DoctorId must be greater than 0");

            RuleFor(x => x.RequestDto.Date)
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Date must be today or in the future.");

            RuleFor(x => x.RequestDto.Time)
                .Must(time => time >= TimeSpan.FromHours(7) && time <= TimeSpan.FromHours(18))
                .WithMessage("Appointment time must be within working hours (7AM - 6PM).");
            RuleFor(x => x.RequestDto)
                .Must(dto => BeInFuture(dto.Date.Date + dto.Time))
                .WithMessage("Appointment must be in the future.");
        }

        private bool BeInFuture(DateTime date)
        {
            return date >= DateTime.UtcNow;
        }
    }
}

