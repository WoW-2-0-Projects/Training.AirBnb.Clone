using Backend_Project.Domain.Entities;
namespace Backend_Project.Domain.Extensions;

public static class UserCredentialsExtension
{
    public static string HashPassword(this UserCredentials userCredentials) =>
        BCrypt.Net.BCrypt.HashPassword(userCredentials.Password);
    public static bool VerifyPassword(this UserCredentials userCredentials, string password) =>
        BCrypt.Net.BCrypt.Verify(password, userCredentials.Password);
}
