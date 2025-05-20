using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Features.Medicines.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Handlers
{
    public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, MedicineDto>
    {
        private readonly IMedicineService _medicineService;
        private readonly IMapper _mapper;

        public CreateMedicineCommandHandler(IMedicineService medicineService, IMapper mapper)
        {
            _medicineService = medicineService;
            _mapper = mapper;
        }

        public async Task<MedicineDto> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Medicine>(request.RequestDto);

            var createdEntity = await _medicineService.CreateMedicine(entity);

            return _mapper.Map<MedicineDto>(createdEntity);
        }
    }

}
