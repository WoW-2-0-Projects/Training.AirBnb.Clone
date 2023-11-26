namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data transfer object (DTO) representing user information.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity
    /// </summary>
    public Guid Id { get; set; } 
    
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
}   