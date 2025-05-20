using AutoMapper;
using ClinicManagementSystem.DTOs.Prescription;
using ClinicManagementSystem.Features.Prescriptions.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Prescriptions.Handlers
{
    public class CreatePrescriptionCommandHandler : IRequestHandler<CreatePrescriptionCommand, PrescriptionDto>
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IMedicineService _medicineService;
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IMapper _mapper;

        public CreatePrescriptionCommandHandler(
            IPrescriptionService prescriptionService,
            IMedicineService medicineService,
            IMedicalRecordService medicalRecordService,
            IMapper mapper)
        {
            _prescriptionService = prescriptionService;
            _medicineService = medicineService;
            _medicalRecordService = medicalRecordService;
            _mapper = mapper;
        }

        public async Task<PrescriptionDto> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RequestDto;

            var record = await _medicalRecordService.GetMedicalRecordById(dto.RecordId);
            if (record == null)
                throw new KeyNotFoundException("Medical record not found");

            var medicine = await _medicineService.GetMedicineById(dto.MedicineId);
            if (medicine == null)
                throw new KeyNotFoundException("Medicine not found");

            var entity = _mapper.Map<Prescription>(dto);
            var result = await _prescriptionService.CreatePrescription(entity);

            return _mapper.Map<PrescriptionDto>(result);
        }
    }

}
