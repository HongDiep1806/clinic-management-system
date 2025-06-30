using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Features.Medicines.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Medicines.Handlers
{
    public class GetAllMedicinesQueryHandler : IRequestHandler<GetAllMedicinesQuery, List<MedicineDto>>
    {
        private readonly IMedicineService _medicineService;
        private readonly IMapper _mapper;

        public GetAllMedicinesQueryHandler(IMedicineService medicineService, IMapper mapper)
        {
            _medicineService = medicineService; 
            _mapper = mapper;
        }

        public async Task<List<MedicineDto>> Handle(GetAllMedicinesQuery request, CancellationToken cancellationToken)
        {
            var medicines = await _medicineService.GetAllMedicines();
            return _mapper.Map<List<MedicineDto>>(medicines);
        }
    }

}
