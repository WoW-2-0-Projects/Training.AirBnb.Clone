using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents user credentials with a hashed password.
/// </summary>
public class UserCredentials
{
    /// <summary>
    /// Gets or sets hashed password for the user.
    /// </summary>
    public string PasswordHash { get; set; } = default!;
}