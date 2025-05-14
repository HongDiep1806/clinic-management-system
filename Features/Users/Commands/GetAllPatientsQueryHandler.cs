using AutoMapper;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Commands
{
    public class GetAllPatientsQueryHandler: IRequestHandler<GetAllPatientsQuery, List<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetAllPatientsQueryHandler(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<List<UserDto>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _userService.GetAllPatients();
            var patientDtos = _mapper.Map<List<UserDto>>(patients);
            return patientDtos;
        }
    }
}
