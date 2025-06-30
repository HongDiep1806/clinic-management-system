using AutoMapper;
using ClinicManagementSystem.DTOs.Prescription;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class PrescriptionProfile: Profile
    {
        public PrescriptionProfile()
        {
            CreateMap<CreatePrescriptionDto, Prescription>();
            CreateMap<Prescription, PrescriptionDto>()
                .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name));


        }
    }
}
