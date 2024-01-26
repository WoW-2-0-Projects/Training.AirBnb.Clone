namespace AirBnB.Domain.Settings;

/// <summary>
/// Represents the cache settings for storing GuestFeedback entities.
/// </summary>
public class GuestFeedbacksCacheSettings
{
    /// <summary>
    /// Gets or sets the absolute expiration time for cached GuestFeedback entities, specified in hours.
    /// </summary>
    public int AbsoluteExpirationTimeInSeconds { get; init; }

    /// <summary>
    /// Gets or sets the sliding expiration time for cached GuestFeedback entities, specified in hours.
    /// </summary>
    public int SlidingExpirationTimeInSeconds { get; init; }
}