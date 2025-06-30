using ClinicManagementSystem.DTOs.Appointment;
using FluentValidation;

namespace ClinicManagementSystem.Features.Appointments.Validators
{
    public class CreateAppointmentByStaffDtoValidator : AbstractValidator<CreateAppointmentByStaffDto>
    {
        public CreateAppointmentByStaffDtoValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0).WithMessage("PatientId must be greater than 0.");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("DoctorId must be greater than 0.");

            RuleFor(x => x.Date)
                .GreaterThan(DateTime.Today).WithMessage("Date must be in the future.");

            RuleFor(x => x.Time)
                .Must(time => time >= TimeSpan.FromHours(7) && time <= TimeSpan.FromHours(18))
                .WithMessage("Appointment time must be within working hours (7AM - 6PM).");

           
        }
    }
}
