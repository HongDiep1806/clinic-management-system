﻿namespace ClinicManagementSystem.DTOs.Auth.Login
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresAt { get; set; }
    }
}
