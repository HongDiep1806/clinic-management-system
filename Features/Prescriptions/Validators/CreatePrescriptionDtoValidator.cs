using ClinicManagementSystem.DTOs.Prescription;
using FluentValidation;

namespace ClinicManagementSystem.Features.Prescriptions.Validators
{
    public class CreatePrescriptionDtoValidator : AbstractValidator<CreatePrescriptionDto>
    {
        public CreatePrescriptionDtoValidator()
        {
            RuleFor(x => x.RecordId).GreaterThan(0);
            RuleFor(x => x.MedicineId).GreaterThan(0);
            RuleFor(x => x.Dosage).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }

}
