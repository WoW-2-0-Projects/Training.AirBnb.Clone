using AirBnB.Application.Common.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides password hashing functionalities
/// </summary>
public class PasswordHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }
    
    public bool ValidatePassword(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
}