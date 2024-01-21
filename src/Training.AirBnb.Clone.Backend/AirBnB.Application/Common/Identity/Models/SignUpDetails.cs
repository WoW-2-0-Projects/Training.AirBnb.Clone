namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
///  Represents signUpDetails details for Authorization
/// </summary>
public class SignUpDetails
{
    /// <summary>
    /// get or set first name of the SignUpDetails
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// get or set last name of the SignUpDetails
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// get or set age of the SignUpDetails
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// get or set email address of the SignUpDetails
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// get or set password of the SignUpDetails
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// get or set phoneNumber of the SignUpDetails
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// get or set autoGeneratePassword of SignUpDetails
    /// </summary>
    public bool AutoGeneratePassword { get; set; }
}