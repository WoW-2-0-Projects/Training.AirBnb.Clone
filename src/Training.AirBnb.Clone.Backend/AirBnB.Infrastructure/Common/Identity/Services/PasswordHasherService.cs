using AirBnB.Application.Common.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace AirBnB.Infrastructure.Common.Identity.Services;
/// <summary>
/// password hashing service entered
/// </summary>
public class PasswordHasherService : IPasswordHasherService
{
    /// <summary>
    /// Hashes a plain-text password using the underlying BC library's secure hashing functionality.
    /// </summary>
    /// <param name="password">The plain-text password to be hashed.</param>
    /// <returns>The hashed password as a string.</returns>
    public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }
    /// <summary>
    /// Validates a plain-text password against a stored hashed password using the underlying BC library's verification functionality.
    /// </summary>
    /// <param name="password">The plain-text password to be validated.</param>
    /// <param name="hashPassword">The stored hashed password to compare against.</param>
    /// <returns>True if the passwords match, false otherwise.</returns>
    public bool ValidatePassword(string password, string hashPassword)
    {
        return BC.Verify(password, hashPassword);
    }
}