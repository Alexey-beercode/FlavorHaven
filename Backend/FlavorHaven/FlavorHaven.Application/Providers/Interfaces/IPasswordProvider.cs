namespace FlavorHaven.Application.Providers.Interfaces;

public interface IPasswordProvider
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string password);
}