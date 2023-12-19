using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a listing with details such as title, description, status, and pricing.
/// </summary>
public class Listing : SoftDeletedEntity
{
    /// <summary>
    /// Gets or sets the title of the listing.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the description associated with the listing.
    /// </summary>
    public Guid? DescriptionId { get; set; }

    /// <summary>
    /// Gets or sets the status of the listing.
    /// </summary>
    public ListingStatus Status { get; set; } = ListingStatus.InProgress;
    
    /// <summary>
    /// Gets or sets the unique identifier for the type of property.
    /// </summary>
    public Guid? PropertyTypeId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the location of the listing.
    /// </summary>
    public Guid? LocationId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the rules associated with the listing.
    /// </summary>
    public Guid? RulesId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the availability details of the listing.
    /// </summary>
    public Guid? AvailabilityId { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the host of the listing.
    /// </summary>
    public Guid? HostId { get; set; }
    
    /// <summary>
    /// Gets or sets the price of the listing.
    /// </summary>
    public decimal? Price { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether instant booking is available for the listing.
    /// </summary>
    public bool InstantBook { get; set; }
}