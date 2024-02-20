namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
/// Represents sign in details by phone
/// </summary>
public class SignInByPhoneDetails
{
    /// <summary>
    /// Gets sign in phone number
    /// </summary>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets sign in password
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Gets sign extended active time
    /// </summary>
    public bool RememberMe { get; set; }
}