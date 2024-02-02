namespace AirBnB.Domain.Constants;

/// <summary>
/// Provides constants representing keys used in caching operations.
/// </summary>
public static class CacheKeyConstants
{
    /// <summary>
    /// Represents the cache key for added GuestFeedback entities.
    /// </summary>
    public const string AddedGuestFeedbacks = "AddedGuestFeedbacks";
    
    /// <summary>
    /// Represents the cache key for deleted GuestFeedback entities.
    /// </summary>
    public const string DeletedGuestFeedbacks = "DeletedGuestFeedbacks";
}