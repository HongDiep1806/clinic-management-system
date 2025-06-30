using ClinicManagementSystem.DTOs.Prescription;
using ClinicManagementSystem.Models;
using MediatR;

namespace ClinicManagementSystem.Features.Prescriptions.Commands
{
    public class CreatePrescriptionCommand: IRequest<PrescriptionDto>
    {
        public CreatePrescriptionDto RequestDto { get; set; }

        public CreatePrescriptionCommand(CreatePrescriptionDto dto)
        {
            RequestDto = dto;
        }
    }
}
