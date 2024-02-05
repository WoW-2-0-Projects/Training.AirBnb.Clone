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
    /// Gets or sets a rating given by the guest.
    /// </summary>
    public Rating Rating { get; set; }
    
    /// <summary>
    /// Gets or sets navigation property of Listing
    /// </summary>
    public virtual Listing Listing { get; set; }
    
    /// <summary>
    /// Gets or sets navigation property of Guest
    /// </summary>
    public virtual User Guest { get; set; }
}