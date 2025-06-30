using ClinicManagementSystem.DTOs.Medicine;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Commands
{
    public class CreateMedicineCommand : IRequest<MedicineDto>
    {
        public CreateMedicineDto RequestDto { get; set; }

        public CreateMedicineCommand(CreateMedicineDto dto)
        {
            RequestDto = dto;
        }
    }

}
