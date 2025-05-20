using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.DTOs.Prescription;
using FluentValidation;

namespace ClinicManagementSystem.Features.MedicalRecords.Validators
{

        public class CreatePrescriptionDtoValidator : AbstractValidator<CreatePrescriptionDto>
        {
            public CreatePrescriptionDtoValidator()
            {
                RuleFor(x => x.RecordId)
                    .GreaterThan(0)
                    .WithMessage("Vui lòng chọn hồ sơ bệnh án hợp lệ");

                RuleFor(x => x.MedicineId)
                    .GreaterThan(0)
                    .WithMessage("Vui lòng chọn thuốc");

                RuleFor(x => x.Dosage)
                    .NotEmpty()
                    .WithMessage("Liều dùng là bắt buộc")
                    .MaximumLength(100)
                    .WithMessage("Liều dùng không được vượt quá 100 ký tự");

                RuleFor(x => x.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Số lượng thuốc phải lớn hơn 0");
            }
        }

    }

