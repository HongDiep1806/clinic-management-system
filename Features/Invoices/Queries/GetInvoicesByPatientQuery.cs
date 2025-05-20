using ClinicManagementSystem.DTOs.Invoice;
using MediatR;

namespace ClinicManagementSystem.Features.Invoices.Queries
{
    public class GetInvoicesByPatientQuery : IRequest<List<PatientInvoiceResponseDto>>
    {
        public int PatientId { get; set; }

        public GetInvoicesByPatientQuery(int patientId)
        {
            PatientId = patientId;
        }
    }
}
