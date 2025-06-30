using AutoMapper;
using ClinicManagementSystem.DTOs.Schedule;
using ClinicManagementSystem.Features.Schedules.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Schedules.Handlers
{
    public class GetDoctorSchedulesQueryHandler : IRequestHandler<GetDoctorSchedulesQuery, List<ScheduleDto>>
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public GetDoctorSchedulesQueryHandler(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        public async Task<List<ScheduleDto>> Handle(GetDoctorSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _scheduleService.GetSchedulesByDoctorId(request.DoctorId);
            return _mapper.Map<List<ScheduleDto>>(schedules);
        }
    }
}
