namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
/// Represents login details for Authorization
/// </summary>
public class SignInDetails
{
    /// <summary>
    /// get or set EmailAddress of the LoginDetails
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// get or set Password of the LoginDetails
    /// </summary>
    public string Password { get; set; } = string.Empty;
}