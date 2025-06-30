using ClinicManagementSystem.Features.Users.Commands;
using FluentValidation;

namespace ClinicManagementSystem.Features.Users.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.CreateUserDto.FullName)
                .NotEmpty().WithMessage("Full name is required");

            RuleFor(x => x.CreateUserDto.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.CreateUserDto.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.CreateUserDto.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^[0-9]{10,15}$").WithMessage("Invalid phone number format");

            RuleFor(x => x.CreateUserDto.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => g == "Male" || g == "Female" || g == "Other")
                .WithMessage("Gender must be Male, Female, or Other");

            RuleFor(x => x.CreateUserDto.Dob)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Today).WithMessage("Date of birth must be in the past");

            RuleFor(x => x.CreateUserDto.RoleId)
                .GreaterThan(0).WithMessage("Role is required");
        }
    }
}
