using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Models;
using MediatR;

namespace ClinicManagementSystem.Features.MedicalRecords.Commands
{
    public class CreateMedicalRecordCommand : IRequest<MedicalRecordDto>
    {
        public CreateMedicalRecordDto RequestDto { get; set; }

        public CreateMedicalRecordCommand(CreateMedicalRecordDto dto)
        {
            RequestDto = dto;
        }
    }

}
