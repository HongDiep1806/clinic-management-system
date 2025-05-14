using ClinicManagementSystem.DTOs.User;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Queries
{
    public class GetAllDoctorsQuery: IRequest<List<UserDto>>
    {
        public GetAllDoctorsQuery()
        {

        }
    }
}
