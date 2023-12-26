using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class AccessTokenRepository(ICacheBroker cacheBroker) : IAccessTokenRepository
{
    public async ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<AccessToken>(accessTokenId.ToString(), cancellationToken);
    }

    public async ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }
} 