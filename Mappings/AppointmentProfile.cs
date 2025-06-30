using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // Patient đặt lịch xong → trả về
            CreateMap<Appointment, BookAppointmentResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.AppointmentDate.ToLocalTime()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentDate.ToString("HH:mm")));

            // DTO chung (dùng cho staff tạo xong)
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentDate.ToString("HH:mm")));

            // Doctor xem lịch hẹn
            CreateMap<Appointment, DoctorAppointmentResponseDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentDate.ToString("HH:mm")));

            // Patient xem lịch hẹn
            CreateMap<Appointment, PatientAppointmentResponseDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.Time, opt => opt.MapFrom(src => src.AppointmentDate.ToString("HH:mm")));
        }

    }
}
