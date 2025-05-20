using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Commands
{
    public class DeleteMedicineCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteMedicineCommand(int id)
        {
            Id = id;
        }
    }

}
