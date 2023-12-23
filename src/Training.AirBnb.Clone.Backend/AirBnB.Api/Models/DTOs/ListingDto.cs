using AirBnB.Domain.Entities;

namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a listing.
/// </summary>
public class ListingDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the listing
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the listing.
    /// </summary>
    public string Name { get; set; } = default!;
    
    public DateOnly BuiltDate { get; set; }

    public Address Address { get; set; } = default!;
    /// <summary>
    /// Gets or sets the price of the listing.
    /// </summary>
    public Money PricePerNight { get; set; } = default!;
}