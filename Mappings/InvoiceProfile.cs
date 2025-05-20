using AutoMapper;
using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class InvoiceProfile:Profile
    {
        public InvoiceProfile()
        {
            CreateMap<CreateInvoiceDto, Invoice>();
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Invoice, PatientInvoiceResponseDto>();

        }
    }
}
