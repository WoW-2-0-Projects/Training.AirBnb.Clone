namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a user profile picture.
/// </summary>
public class UserProfilePictureDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the user profile picture.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the URL of the user profile picture.
    /// </summary>
    public string ImageUrl { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the unique identifier of the user associated with this profile picture.
    /// </summary>
    public Guid UserId { get; set; }
}