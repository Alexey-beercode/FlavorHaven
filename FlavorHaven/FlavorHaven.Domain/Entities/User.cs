using FlavorHaven.Domain.Common;

namespace FlavorHaven.Domain.Entities;

public class User:BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public decimal Balance { get; set; }
}