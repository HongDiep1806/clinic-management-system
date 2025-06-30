using AutoMapper;
using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Features.Appointments.Queries;
using ClinicManagementSystem.Features.Invoices.Queries;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Features.Invoices.Handlers
{
    public class GetInvoicesByPatientQueryHandler : IRequestHandler<GetInvoicesByPatientQuery, List<PatientInvoiceResponseDto>>
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IMapper _mapper;
        public GetInvoicesByPatientQueryHandler(IInvoiceService invoiceService, IMapper mapper)
        {
            _invoiceService = invoiceService;
            _mapper = mapper;
        }

        public async Task<List<PatientInvoiceResponseDto>> Handle(GetInvoicesByPatientQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceService.GetPatientaInvoices(request.PatientId);

            return _mapper.Map<List<PatientInvoiceResponseDto>>(invoices);
        }
    }
}
