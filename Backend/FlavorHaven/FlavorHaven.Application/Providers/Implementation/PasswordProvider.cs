using FlavorHaven.Application.Providers.Interfaces;

namespace FlavorHaven.Application.Providers.Implementation;

public class PasswordProvider : IPasswordProvider
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}