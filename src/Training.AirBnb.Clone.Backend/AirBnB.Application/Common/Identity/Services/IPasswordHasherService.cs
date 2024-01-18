namespace AirBnB.Application.Common.Identity.Services;
/// <summary>
/// Service interface for password Hasher operations
/// </summary>
public interface IPasswordHasherService
{
    /// <summary>
    /// Hashes a plain-text password using a secure, one-way hashing algorithm.
    /// </summary>
    /// <param name="password"> The plain-text password to be hashed.</param>
    /// <returns>The hashed password as a string.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Validates a plain-text password against a previously generated hashed password.
    /// </summary>
    /// <param name="password">The plain-text password to be validated.</param>
    /// <param name="hashPassword">The stored hashed password to compare against.</param>
    /// <returns>True if the passwords match, false otherwise.</returns>
    bool ValidatePassword(string password, string hashPassword);
}