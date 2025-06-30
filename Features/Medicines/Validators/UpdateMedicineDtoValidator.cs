using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Features.Medicines.Commands;
using FluentValidation;

namespace ClinicManagementSystem.Features.Medicines.Validators
{
    public class UpdateMedicineDtoValidator : AbstractValidator<UpdateMedicineCommand>
    {
        public UpdateMedicineDtoValidator()
        {
            RuleFor(x => x.RequestDto.Name)
                .NotEmpty().WithMessage("Medicine name is required");

            RuleFor(x => x.RequestDto.Unit)
                .NotEmpty().WithMessage("Unit is required");

            RuleFor(x => x.RequestDto.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity must be >= 0");

            RuleFor(x => x.RequestDto.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

}
