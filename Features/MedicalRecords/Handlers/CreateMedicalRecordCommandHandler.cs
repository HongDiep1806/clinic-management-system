using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.DTOs.MedicalRecord;
using ClinicManagementSystem.Features.MedicalRecords.Commands;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.Services;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Features.MedicalRecords.Handlers
{
    public class CreateMedicalRecordCommandHandler : IRequestHandler<CreateMedicalRecordCommand, MedicalRecordDto>
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public CreateMedicalRecordCommandHandler(IMedicalRecordService medicalRecordService, IMapper mapper, IAppointmentService appointmentService)

        {
            _medicalRecordService = medicalRecordService;
            _mapper = mapper;
            _appointmentService = appointmentService;
        }

        public async Task<MedicalRecordDto> Handle(CreateMedicalRecordCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAppointmentById(request.RequestDto.AppointmentId);
            if (appointment == null)
                throw new KeyNotFoundException("Appointment not found");

            var record = _mapper.Map<MedicalRecord>(request.RequestDto);
            record.CreatedAt = DateTime.UtcNow;

            var result = await _medicalRecordService.CreateMedicalRecord(record);

            return _mapper.Map<MedicalRecordDto>(result);
        }
    }

}
