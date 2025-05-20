using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Features.MedicalRecords.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.MedicalRecords.Handlers
{
    public class GetMedicalRecordsByPatientQueryHandler : IRequestHandler<GetMedicalRecordsByPatientQuery, List<MedicalRecordDto>>
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IMapper _mapper;

        public GetMedicalRecordsByPatientQueryHandler(IMedicalRecordService medicalRecordService, IMapper mapper)
        {
            _mapper = mapper;
            _medicalRecordService = medicalRecordService;
        }

        public async Task<List<MedicalRecordDto>> Handle(GetMedicalRecordsByPatientQuery request, CancellationToken cancellationToken)
        {
            var records = await _medicalRecordService.GetMedicalRecordsByPatientId(request.PatientId);

            return _mapper.Map<List<MedicalRecordDto>>(records);
        }
    }

}
