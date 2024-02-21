using AirBnB.Domain.Entities;

namespace AirBnB.Api.Models.DTOs;

/// <summary>
/// Represents a data transfer object (DTO) for caching guest feedback information.
/// </summary>
public class GuestFeedbackCacheDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the listing associated with the guest feedback.
    /// </summary>
    public Guid ListingId { get; set; }

    /// <summary>
    /// Gets or sets the rating given by the guest.
    /// </summary>
    public Rating Rating { get; set; } = default!;
}