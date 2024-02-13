using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class RefreshTokenRepository(ICacheBroker cacheBroker) : IRefreshTokenRepository
{
    public async ValueTask<RefreshToken> CreateAsync(
        RefreshToken refreshToken, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(refreshToken.ExpiryTime - DateTimeOffset.UtcNow, null);
  
        await cacheBroker.SetAsync($"{nameof(RefreshToken)}-{refreshToken.Token}", refreshToken, cacheEntryOptions, cancellationToken);

        return refreshToken;
    }

    public ValueTask<RefreshToken?> GetByValueAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default) =>
    cacheBroker.GetAsync<RefreshToken>($"{nameof(RefreshToken)}-{refreshTokenValue}", cancellationToken: cancellationToken);

    public ValueTask RemoveAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default) =>
    cacheBroker.DeleteAsync($"{nameof(RefreshToken)}-{refreshTokenValue}", cancellationToken: cancellationToken);
}
