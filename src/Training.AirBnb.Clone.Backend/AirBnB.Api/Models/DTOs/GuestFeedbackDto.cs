using AirBnB.Domain.Entities;

namespace AirBnB.Api.Models.DTOs;

public class GuestFeedbackDto
{
    /// <summary>
    /// Gets or sets the guest's comment regarding the experience.
    /// </summary>
    public string Comment { get; set; } = default!;

    /// <summary>
    /// Gets or sets the unique identifier of the listing associated with the guest feedback.
    /// </summary>
    public Guid ListingId { get; set; }

    /// <summary>
    /// Gets or sets a rating given by the guest.  
    /// </summary>
    public Rating Rating { get; set; }
    
    /// <summary>
    /// Gets or sets the created time of the feedback
    /// </summary>
    public DateTimeOffset CreatedTime { get; set; }

    /// <summary>
    /// Gets or sets the name of the Guest
    /// </summary>
    public string GuestName { get; set; } = default!;
}