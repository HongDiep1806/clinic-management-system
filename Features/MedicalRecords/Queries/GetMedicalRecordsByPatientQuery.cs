using ClinicManagementSystem.DTOs.MedicalRecord;
using MediatR;

namespace ClinicManagementSystem.Features.MedicalRecords.Queries
{
    public class GetMedicalRecordsByPatientQuery : IRequest<List<MedicalRecordDto>>
    {
        public int PatientId { get; set; }

        public GetMedicalRecordsByPatientQuery(int patientId)
        {
            PatientId = patientId;
        }
    }

}
