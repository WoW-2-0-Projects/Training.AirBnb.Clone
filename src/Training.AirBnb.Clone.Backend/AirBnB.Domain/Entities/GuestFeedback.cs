using AirBnB.Domain.Common.Entities;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a guest's feedback entity, including individual aspect ratings and an overall rating.
/// </summary>
public class GuestFeedback : AuditableEntity
{
    /// <summary>
    /// Gets or sets the guest's comment regarding the experience.
    /// </summary>
    public string Comment { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier of the guest submitting the feedback.
    /// </summary>
    public Guid GuestId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the listing associated with the guest feedback.
    /// </summary>
    public Guid ListingId { get; set; }
    
    /// <summary>
    /// Gets or sets the cleanliness rating provided by the guest (1 to 5).
    /// </summary>
    public byte Cleanliness { get; set; }

    /// <summary>
    /// Gets or sets the accuracy rating provided by the guest (1 to 5).
    /// </summary>
    public byte Accuracy { get; set; }

    /// <summary>
    /// Gets or sets the check-in rating provided by the guest (1 to 5).
    /// </summary>
    public byte CheckIn { get; set; }

    /// <summary>
    /// Gets or sets the communication rating provided by the guest (1 to 5).
    /// </summary>
    public byte Communication { get; set; }

    /// <summary>
    /// Gets or sets the location rating provided by the guest (1 to 5).
    /// </summary>
    public byte Location { get; set; }

    /// <summary>
    /// Gets or sets the value rating provided by the guest (1 to 5).
    /// </summary>
    public byte Value { get; set; }

    /// <summary>
    /// Gets or sets the calculated overall rating based on the individual aspect ratings.
    /// </summary>
    public double OverallRating { get; set; }
    
    /// <summary>
    /// Gets or sets navigation property of Listing
    /// </summary>
    public virtual Listing Listing { get; set; }
    
    /// <summary>
    /// Gets or sets navigation property of Guest
    /// </summary>
    public virtual User Guest { get; set; }
}