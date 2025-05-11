using AutoMapper;
using ClinicManagementSystem.DAL;
using ClinicManagementSystem.Models;
using ClinicManagementSystem.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ClinicManagementSystem.Services;
using ClinicManagementSystem.Features.Users.Commands;
using ClinicManagementSystem.Features.Users.Commands;

namespace ClinicManagementSystem.Features.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterUserCommandHandler(IMapper mapper, IUserService userService, IPasswordHasher<User> passwordHasher)
        {
            _mapper = mapper;
            _userService = userService;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.CreateUserDto);
            user.HashPassword = _passwordHasher.HashPassword(user, request.CreateUserDto.Password);
            var newUser = await _userService.CreateUser(user);
            await _userService.AssignRoleToUser(newUser.UserId, request.CreateUserDto.RoleId);
            var userDto = _mapper.Map<UserDto>(newUser);
            return userDto;
        }
    }
}
