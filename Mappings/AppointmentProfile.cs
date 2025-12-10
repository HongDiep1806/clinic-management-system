using AutoMapper;
using ClinicManagementSystem.DTOs.Appointment;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem.Mappings
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            // Mapping khi bệnh nhân hoặc staff vừa đặt lịch xong
            CreateMap<Appointment, BookAppointmentResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            // DTO dùng để hiển thị chung trong bảng (Admin / Receptionist)
            CreateMap<Appointment, AppointmentDto>()
                 .ForMember(dest => dest.PatientName,
                     opt => opt.MapFrom(src => src.Patient.FullName))
                 .ForMember(dest => dest.DoctorName,
                     opt => opt.MapFrom(src => src.Doctor.FullName))
                 .ForMember(dest => dest.Status,
                     opt => opt.MapFrom(src => src.Status.ToString()))
                 .ForMember(dest => dest.Date,
                     opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                 .ForMember(dest => dest.CreatedAt,
                     opt => opt.MapFrom(src => src.CreatedAt))
                 .ForMember(dest => dest.DoctorId,
                     opt => opt.MapFrom(src => src.DoctorId))

                 // ⭐ SNAPSHOT FIELDS
                 .ForMember(dest => dest.DepartmentId,
                     opt => opt.MapFrom(src => src.DepartmentId))
                 .ForMember(dest => dest.DepartmentName,
                     opt => opt.MapFrom(src => src.DepartmentName));




            // Doctor xem lịch hẹn (không cần time — khám theo queue)
            CreateMap<Appointment, DoctorAppointmentResponseDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            // Patient xem danh sách lịch hẹn
            CreateMap<Appointment, PatientAppointmentResponseDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.Doctor.FullName))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));
        }
    }
}
