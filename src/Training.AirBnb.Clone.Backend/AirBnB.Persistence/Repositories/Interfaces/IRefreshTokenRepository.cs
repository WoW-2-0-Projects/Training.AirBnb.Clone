using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for accessing and managing RefreshToken entities in a data store.
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    /// Asynchronously creates a new RefreshToken entity.
    /// </summary>
    /// <param name="refreshToken">The RefreshToken entity to be created</param>
    /// <param name="saveChanges">Indicates whether changes should be saved to the underlying data store (default is true)</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed (default is none)</param>
    /// <returns>A ValueTask representing the asynchronous operation, returning the created RefreshToken</returns>
    ValueTask<RefreshToken> CreateAsync(
        RefreshToken refreshToken, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<RefreshToken?> GetByValueAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenValue"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask RemoveAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default);
}
