namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing information about a listing media file.
/// </summary>
public class ListingMediaFileDto
{
    /// <summary>
    /// Gets or initializes the unique identifier of the listing media file.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or initializes the URL of the image associated with the listing media file.
    /// </summary>
    public string ImageUrl { get; init; } = default!;

    /// <summary>
    /// Gets or initializes the order number of the listing media file.
    /// </summary>
    public byte OrderNumber { get; init; }

    /// <summary>
    /// Gets or sets the unique identifier of the listing associated with the media file.
    /// </summary>
    public Guid ListingId { get; set; } 
}