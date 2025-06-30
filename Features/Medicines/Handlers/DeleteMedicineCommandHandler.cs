using ClinicManagementSystem.Features.Medicines.Commands;
using ClinicManagementSystem.Models.Deleted;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Handlers
{
    public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand, bool>
    {
        private readonly IMedicineService _medicineService;
        public DeleteMedicineCommandHandler(IMedicineService medicineService)
        {
            _medicineService = medicineService; 
        }
        public async Task<bool> Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
        {
            return await _medicineService.DeleteMedicine(request.Id);
        }

    }
}
