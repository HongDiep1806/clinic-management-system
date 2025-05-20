using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Features.Medicines.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Handlers
{
    public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand, bool>
    {
        private readonly IMedicineService _medicineService;
        private readonly IMapper _mapper;

        public UpdateMedicineCommandHandler(IMedicineService medicineService, IMapper mapper)
        {
            _medicineService = medicineService;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
        {
            var medicine = await _medicineService.GetMedicineById(request.RequestDto.MedicineId);
            if (medicine == null)
                throw new KeyNotFoundException("Medicine not found.");

            var updatedMedicine = _mapper.Map<Medicine>(request.RequestDto);
            return await _medicineService.UpdateMedicine(medicine.MedicineId, updatedMedicine);

        }
    }

}
