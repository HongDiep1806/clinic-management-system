using AutoMapper;
using ClinicManagementSystem.DTOs.Department;
using ClinicManagementSystem.DTOs.Invoice;
using ClinicManagementSystem.DTOs.User;
using ClinicManagementSystem.Features.Users.Queries;
using ClinicManagementSystem.Services;
using MediatR;

namespace ClinicManagementSystem.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IRequest<List<DepartmentDto>>
    {
        public GetAllDepartmentsQuery() { }
    }
}
