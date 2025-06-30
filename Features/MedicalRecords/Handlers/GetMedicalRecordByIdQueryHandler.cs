using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Features.MedicalRecords.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.MedicalRecords.Handlers
{
    public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, MedicalRecordDto>
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IMapper _mapper;

        public GetMedicalRecordByIdQueryHandler(IMedicalRecordService medicalRecordService, IMapper mapper)
        {
           _medicalRecordService = medicalRecordService;
            _mapper = mapper;
        }

        public async Task<MedicalRecordDto> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var record = await _medicalRecordService.GetMedicalRecordById(request.Id);

            if (record == null)
                throw new KeyNotFoundException("Medical record not found");

            return _mapper.Map<MedicalRecordDto>(record);
        }
    }

}
