using AirBnB.Domain.Common.Caching;

namespace AirBnB.Persistence.Caching;

public interface ICacheBroker
{
    ValueTask<T?> GetAsync<T>(string key);

    ValueTask<bool> TryGetAsync<T>(string key, out T? value);

    ValueTask<T?> GetOrSetAsync<T>(string key, Func<Task<T>> valueFactory, CacheEntryOptions? cacheEntryOptions = default);

    ValueTask SetAsync<T>(string key, T value, CacheEntryOptions? entryOptions = default);

    ValueTask DeleteAsync(string key);
}