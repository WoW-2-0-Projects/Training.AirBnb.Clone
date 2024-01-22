using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a linking entity between a Listing and a ListingCategory.
/// This entity is used to establish a many-to-many relationship, associating
/// a Listing with one or more ListingCategories, and vice versa.
/// </summary>
public class ListingCategoryAssociation : Entity
{
    /// <summary>
    /// Gets or sets the unique identifier of the associated Listing.
    /// </summary>
    public Guid ListingId { get; set; }

    /// <summary>
    /// Gets or sets the Listing associated with this entity.
    /// </summary>
    public Listing Listing { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier of the associated ListingCategory.
    /// </summary>
    public Guid ListingCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the ListingCategory associated with this entity.
    /// </summary>
    public ListingCategory ListingCategory { get; set; }
}