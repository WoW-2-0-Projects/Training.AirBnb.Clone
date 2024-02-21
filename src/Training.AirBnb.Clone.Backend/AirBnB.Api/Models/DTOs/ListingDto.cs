using AirBnB.Domain.Entities;

namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a listing.
/// </summary>
public class ListingDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the listing.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the listing.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date when the listing was built.
    /// </summary>
    public DateOnly BuiltDate { get; set; }

    /// <summary>
    /// Gets or sets the address associated with the listing.
    /// </summary>
    public Address Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the price per night for the listing.
    /// </summary>
    public Money PricePerNight { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the rating of the listing.
    /// </summary>
    public Rating? Rating { get; set; }
    
    /// <summary>
    /// Gets or sets the urls of the images associated with the listing.
    /// </summary>
    public List<string> ImagesUrls { get; set; }

    /*public ListingMediaFile ImagesStorageFile { get; set; }*/
}
