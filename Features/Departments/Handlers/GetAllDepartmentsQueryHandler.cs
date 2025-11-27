using AutoMapper;
using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.Features.Departments.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Departments.Handlers
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery,List<DepartmentDto>>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }
        public async Task<List<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllDepartments();

            return _mapper.Map<List<DepartmentDto>>(departments);
        }
    }
}
