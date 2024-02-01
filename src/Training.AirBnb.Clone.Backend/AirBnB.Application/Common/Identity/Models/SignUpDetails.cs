namespace AirBnB.Application.Common.Identity.Models;

/// <summary>
///  Represents sign up details for authorization
/// </summary>
public class SignUpDetails
{
    /// <summary>
    /// Gets or sets first name of the sign up details
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets last name of the sign up details
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets age of the sign up details
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Gets or sets email address of the sign up details
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Gets or sets password of the sign up details
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Gets or sets phone number of the sign up details
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets auto generate password of sign up Details
    /// </summary>
    public bool AutoGeneratePassword { get; set; }
}