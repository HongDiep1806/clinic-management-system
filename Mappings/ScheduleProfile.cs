using AutoMapper;
using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<CreateScheduleDto, Schedule>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.Ignore()) 
                .AfterMap((src, dest) =>
                {
                    if (!Enum.TryParse<WeekDay>(src.DayOfWeek, true, out var parsed))
                        throw new ArgumentException("Invalid DayOfWeek: " + src.DayOfWeek);
                    dest.DayOfWeek = parsed;
                });

            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.DayOfWeek,
                    opt => opt.MapFrom(src => src.DayOfWeek.ToString()));
        }
    }
}
