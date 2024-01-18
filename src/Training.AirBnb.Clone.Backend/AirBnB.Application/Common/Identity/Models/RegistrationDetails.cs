namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
///  Represents registration details for Authorization
/// </summary>
public class RegistrationDetails
{
    /// <summary>
    /// get or set first name of the RegistrationDetails
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// get or set last name of the RegistrationDetails
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// get or set age of the RegistrationDetails
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// get or set email address of the RegistrationDetails
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// get or set password of the RegistrationDetails
    /// </summary>
    public string Password { get; set; } = default!;
}