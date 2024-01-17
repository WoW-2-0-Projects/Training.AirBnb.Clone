using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a listing entity.
/// </summary>
public class Listing : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the listing.
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
}
