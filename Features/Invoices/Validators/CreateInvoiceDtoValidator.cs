using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Features.Invoices.Commands;
using ClinicManagementSystem.Models;
using FluentValidation;

namespace ClinicManagementSystem.Features.Invoices.Validators
{
    public class CreateInvoiceDtoValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceDtoValidator()
        {
            RuleFor(x => x.RequestDto.RecordId)
                .GreaterThan(0)
                .WithMessage("RecordId không hợp lệ");

            RuleFor(x => x.RequestDto.PaymentMethod)
                .Must(BeAValidMethod)
                .WithMessage("Method must be one of methods: Cash, Card, Bank Transfer");
        }

        private bool BeAValidMethod(string method)
        {
            return Enum.TryParse(typeof(PaymentMethod), method, true, out _);
        }
    }

}
