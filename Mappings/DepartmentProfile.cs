using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Deleted;

namespace ClinicManagementSystem.Mappings
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            // Map từ Department → DepartmentDto
            CreateMap<Department, DepartmentDto>()
                .ForMember(dest => dest.Status, opt => opt.Ignore());
            // status sẽ được set thủ công

            // Map từ DeletedDepartment → DepartmentDto
            CreateMap<DeletedDepartment, DepartmentDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Inactive"));
        }
    }
}
