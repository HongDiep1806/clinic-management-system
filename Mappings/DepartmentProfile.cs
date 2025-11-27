using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
