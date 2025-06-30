using ClinicManagementSystem.DTOs.MedicalRecord;
using MediatR;

namespace ClinicManagementSystem.Features.MedicalRecords.Queries
{
    public class GetMedicalRecordByIdQuery : IRequest<MedicalRecordDto>
    {
        public int Id { get; set; }

        public GetMedicalRecordByIdQuery(int id)
        {
            Id = id;
        }
    }

}
