using ClinicManagementSystem.DTOs.Medicine;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Commands
{
    public class UpdateMedicineCommand : IRequest<bool>
    {
        //public int Id { get; set; }
        public UpdateMedicineDto RequestDto { get; set; }

        public UpdateMedicineCommand(UpdateMedicineDto dto)
        {
            //Id = id;
            RequestDto = dto;
        }
    }

}
