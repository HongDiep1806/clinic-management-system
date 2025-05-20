using AutoMapper;
using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class MedicalRecordProfile:Profile
    {
        public MedicalRecordProfile()
        {
            CreateMap<MedicalRecord, MedicalRecordDto>();
            CreateMap<CreateMedicalRecordDto, MedicalRecord>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) 
                .ForMember(dest => dest.MedicalRecordId, opt => opt.Ignore()); 
        }
    }
}
