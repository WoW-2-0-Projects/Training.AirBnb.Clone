namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
/// Represents login details for authorization
/// </summary>
public class SignInDetails
{
    /// <summary>
    /// Gets or sets email address of the login details
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets password of the Login details
    /// </summary>
    public string Password { get; set; } = string.Empty;
}