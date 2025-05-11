using ClinicManagementSystem.DTOs.User;
using MediatR;

namespace ClinicManagementSystem.Features.Users.Commands
{
    public class RegisterUserCommand:IRequest<UserDto>
    {
        public CreateUserDto CreateUserDto { get; set; }

    }
}
