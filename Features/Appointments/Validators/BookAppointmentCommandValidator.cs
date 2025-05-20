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

            RuleFor(x => x.RequestDto.AppointmentDate)
                .Must(BeInFuture)
                .WithMessage("Appointment date must be in the future or now");
        }

        private bool BeInFuture(DateTime date)
        {
            return date >= DateTime.UtcNow;
        }
    }
}
