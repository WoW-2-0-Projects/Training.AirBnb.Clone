namespace AirBnB.Infrastructure.Common.Settings;

/// <summary>
/// Represents the configuration settings for caching in a system.
/// </summary>
public class CacheSettings
{
    /// <summary>
    /// Gets or sets the absolute expiration time for cached items, measured in seconds.
    /// </summary>
    public int AbsoluteExpirationInSeconds { get; set; }

    /// <summary>
    ///Gets or sets the sliding expiration time for cached items, measured in seconds. 
    /// </summary>
    public int SlidingExpirationInSeconds { get; set; }
}