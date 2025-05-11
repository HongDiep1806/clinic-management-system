﻿namespace ClinicManagementSystem.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedByIp { get; set; }

        public DateTime? RevokedAt { get; set; }

        public string? RevokedByIp { get; set; }

        public string? ReplacedByToken { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public bool IsRevoked => RevokedAt != null;

        public bool IsActive => !IsRevoked && !IsExpired;

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
