using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Features.Invoices.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Features.Invoices.Handlers
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceDto>
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IMapper mapper, IMedicalRecordService medicalRecordService, IInvoiceService invoiceService)
        {
            _mapper = mapper;
            _medicalRecordService = medicalRecordService;
            _invoiceService = invoiceService;
        }

        public async Task<InvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RequestDto;

            var record = await _medicalRecordService.GetMedicalRecordByIdIncludePres(dto.RecordId);

            if (record == null)
                throw new KeyNotFoundException("Medical record not found");

            if (await _invoiceService.IsInvoiceOfRecordExisted(record.MedicalRecordId))
                throw new InvalidOperationException("Invoice already exists for this record");

            float total = record.Prescriptions.Sum(p => p.Medicine.Price * p.Quantity);

            if (!Enum.TryParse<PaymentMethod>(dto.PaymentMethod, true, out var parsedMethod))
            {
                throw new ArgumentException("Invalid method");

            }

            var invoice = new Invoice
            {
                RecordId = dto.RecordId,
                TotalAmount = total,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = parsedMethod,
                PaymentStatus = "Paid"
            };


            var result = await _invoiceService.CreateInvoice(invoice);  

            return _mapper.Map<InvoiceDto>(result);
        }
    }

}
