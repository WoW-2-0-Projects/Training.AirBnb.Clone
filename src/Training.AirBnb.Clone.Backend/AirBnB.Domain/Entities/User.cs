using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a user entity
/// </summary>
public sealed class User : AuditableEntity
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
    /// Gets or sets the user role type
    /// </summary>
    public IList<Role> Roles  { get; set; }
    
    /// <summary>
    /// Gets or sets user activation
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets user's email address verification
    /// </summary>
    public bool IsEmailAddressVerified { get; set; }
    
    /// <summary>
    ///Gets or sets user phone number 
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
    
    /// <summary>
    /// User settings property for includes
    /// </summary>
    public UserSettings UserSettings { get; set; }

    /// <summary>
    /// Gets or sets listings owned by the user.
    /// </summary>
    public List<Listing> Listings { get; set; }
    
    /// <summary>
    /// Gets or sets the user credentials
    /// </summary>
    public UserCredentials UserCredentials { get; set; }

    /// <summary>
    /// Gets or sets the user profile picture
    /// </summary>
    public UserProfileMediaFile ProfilePicture { get; set; }
}