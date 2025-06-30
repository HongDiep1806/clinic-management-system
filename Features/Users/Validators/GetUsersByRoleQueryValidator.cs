using ClinicManagementSystem.Features.Users.Queries;
using FluentValidation;

namespace ClinicManagementSystem.Features.Users.Validators
{
    public class GetUsersByRoleQueryValidator : AbstractValidator<GetUsersByRoleQuery>
    {
        public GetUsersByRoleQueryValidator()
        {
            var allowedRoles = new[] { "admin", "doctor", "patient", "receptionist" };

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")
                .Must(role => allowedRoles.Contains(role.ToLower()))
                .WithMessage("Invalid role name. It must be one of: Admin, Doctor, Patient, Receptionist");
        }
    }

}
