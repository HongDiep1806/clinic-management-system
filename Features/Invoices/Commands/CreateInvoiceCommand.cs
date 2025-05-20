using ClinicManagementSystem.DTOs.Invoice;
using MediatR;

namespace ClinicManagementSystem.Features.Invoices.Commands
{
    public class CreateInvoiceCommand : IRequest<InvoiceDto>
    {
        public CreateInvoiceDto RequestDto { get; set; }

        public CreateInvoiceCommand(CreateInvoiceDto dto)
        {
            RequestDto = dto;
        }
    }

}
