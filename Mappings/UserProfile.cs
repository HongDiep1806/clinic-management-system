using AutoMapper;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
             .ForMember(dest => dest.DepartmentName,
                 opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
             .ForMember(dest => dest.Roles,
                 opt => opt.MapFrom(src =>
                     src.UserRoles != null
                         ? src.UserRoles
                             .Where(r => r != null && r.Role != null)
                             .Select(r => r.Role.RoleName)
                             .ToList()
                         : new List<string>()));



            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.HashPassword, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<User, UserSummaryDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRoles.First().Role.RoleName))
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null));

        }
    }
}
