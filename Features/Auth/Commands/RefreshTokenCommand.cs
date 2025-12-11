using ClinicManagementSystem.DTOs.Auth.Login;
using MediatR;

public class RefreshTokenCommand : IRequest<LoginResponseDto>
{
    public string? RefreshToken { get; set; }

    public RefreshTokenCommand() { }

    public RefreshTokenCommand(string token)
    {
        RefreshToken = token;
    }
}
