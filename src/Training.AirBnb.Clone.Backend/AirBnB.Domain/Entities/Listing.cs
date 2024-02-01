using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a listing entity.
/// </summary>
public class Listing : AuditableEntity, ICreationAuditableEntity, IDeletionAuditableEntity
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

    /// <summary>
    /// Gets or sets the ID of the host associated with the listing.
    /// </summary>
    public Guid HostId { get; set; }
    
    /// <summary>
    /// Gets or sets the user ID who created the listing.
    /// </summary>
    public Guid CreatedByUserId { get; set; }
    
    /// <summary>
    /// Gets or sets the user ID who deleted the listing.
    /// </summary>
    public Guid? DeletedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the navigation user property who owns this listing.
    /// </summary>
    public virtual User Host { get; set; }

    /// <summary>
    /// Navigation property that stores the categories of a listing
    /// </summary>
    public List<ListingCategory> ListingCategories { get; set; }
}
