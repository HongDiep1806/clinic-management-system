using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, BookAppointmentResponseDto>()
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate.ToLocalTime()));
        }
    }
}
