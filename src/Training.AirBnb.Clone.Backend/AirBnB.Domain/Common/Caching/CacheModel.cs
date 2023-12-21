namespace AirBnB.Domain.Common.Caching;

/// <summary>
///  Represents an abstract base class for cache models, providing a common interface for caching operations.
/// </summary>
public abstract class CacheModel
{
    /// <summary>
    ///  Gets the unique cache key associated with the cache model.
    /// </summary>
    public abstract string CacheKey { get; }
}