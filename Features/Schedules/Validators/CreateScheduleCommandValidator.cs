using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Models;
using FluentValidation;

namespace ClinicManagementSystem.Features.Schedules.Validators
{
    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleDto>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .GreaterThan(0).WithMessage("DoctorId must be greater than 0.");

            RuleFor(x => x.DayOfWeek)
             .NotEmpty().WithMessage("DayOfWeek is required.")
             .Must(BeAValidDayOfWeek).WithMessage("DayOfWeek must be a valid value.");


            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime).WithMessage("StartTime must be before EndTime.");

            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("RoomNumber is required.")
                .MaximumLength(10).WithMessage("RoomNumber must not exceed 10 characters.");
        }

        private bool BeAValidDayOfWeek(string day)
        {
            return Enum.TryParse<WeekDay>(day, true, out _); 
        }

    }
}
