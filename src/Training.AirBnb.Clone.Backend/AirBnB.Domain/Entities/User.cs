using AirBnB.Domain.Common;
namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a user entity
/// </summary>
public sealed class User : SoftDeletedEntity
{
    /// <summary>
    /// Gets or sets the first name of the user
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user 
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the user
    /// </summary>
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    ///Gets or sets the password of the user 
    /// </summary>
    public string Password { get; set; } = default!;
    
    //TODO: Add User Role for Identity 
    
    /// <summary>
    /// Gets or sets user activation
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    ///Gets or sets unique identifier of user's phone number 
    /// </summary>
    public Guid PhoneNumberId { get; set; } = default!;
}