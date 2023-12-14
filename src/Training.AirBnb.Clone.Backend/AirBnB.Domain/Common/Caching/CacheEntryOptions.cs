namespace AirBnB.Domain.Common.Caching;

public class CacheEntryOptions
{
    public TimeSpan? AbsoluteExpirationRelativeToNow { get; init; }
    
    public TimeSpan? SlidingExpiration { get; init; }

    public CacheEntryOptions()
    {
    }

    public CacheEntryOptions(TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration)
    {
        AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        SlidingExpiration = slidingExpiration;
    }
}