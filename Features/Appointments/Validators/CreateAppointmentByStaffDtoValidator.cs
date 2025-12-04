using ClinicManagementSystem.DTOs.Appointment;
using FluentValidation;

namespace ClinicManagementSystem.Features.Appointments.Validators
{
    public class CreateAppointmentByStaffDtoValidator : AbstractValidator<CreateAppointmentByStaffDto>
    {
        public CreateAppointmentByStaffDtoValidator()
        {
            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId must be greater than 0.");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("DoctorId must be greater than 0.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required.")
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("Date must be today or in the future.");

            // ❌ Không kiểm tra thời gian nữa
            // Vì flow mới không dùng Time
        }
    }
}
