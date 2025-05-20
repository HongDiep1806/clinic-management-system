using ClinicManagementSystem.Features.Medicines.Commands;
using FluentValidation;

namespace ClinicManagementSystem.Features.Medicines.Validators
{
    public class DeleteMedicineDtoValidator : AbstractValidator<DeleteMedicineCommand>
    {
        public DeleteMedicineDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Invalid medicine ID");
        }
    }

}
