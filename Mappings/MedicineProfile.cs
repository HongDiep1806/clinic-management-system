using AutoMapper;
using ClinicManagementSystem.DTOs.Medicine;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile()
        {
            CreateMap<CreateMedicineDto, Medicine>();
            CreateMap<Medicine, MedicineDto>();
            CreateMap<UpdateMedicineDto, Medicine>();

        }
    }
}
