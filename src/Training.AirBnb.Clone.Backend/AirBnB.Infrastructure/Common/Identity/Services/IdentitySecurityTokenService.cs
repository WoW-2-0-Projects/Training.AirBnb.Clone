using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service for managing access tokens using an access token repository.
/// </summary>
public class IdentitySecurityTokenService(
    IAccessTokenRepository accessTokenRepository, 
    IRefreshTokenRepository refreshTokenRepository,
    IValidator<RefreshToken> refreshTokenValidator) : 
    IIdentitySecurityTokenService
{
    public ValueTask<AccessToken> CreateAccessTokenAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.CreateAsync(accessToken, saveChanges, cancellationToken);
    }

    public ValueTask<RefreshToken> CreateRefreshTokenAsync(
        RefreshToken refreshToken, 
        bool saveChanges = true, 
        CancellationToken cancellationToken = default)
    {
        var validationResult = refreshTokenValidator.Validate(refreshToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return refreshTokenRepository.CreateAsync(refreshToken, saveChanges, cancellationToken);
    }

    public ValueTask<AccessToken?> GetAccessTokenByIdAsync(
        Guid accessTokenId, 
        CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.GetByIdAsync(accessTokenId, cancellationToken);
    }

    public ValueTask<RefreshToken?> GetRefreshTokenByValueAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default) =>
    refreshTokenRepository.GetByValueAsync(refreshTokenValue, cancellationToken);

    public async ValueTask RevokeAccessTokenAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        // Retrieve the access token by its ID.
        var accessToken = await GetAccessTokenByIdAsync(accessTokenId, cancellationToken);

        // Check if the access token exists; throw an exception if not found.
        if (accessToken is null)
            throw new InvalidOperationException($"Access with id {accessTokenId} not found.");

        // Set the IsRevoked property to true and update the access token in the repository.
        accessToken.IsRevoked = true;
        await accessTokenRepository.UpdateAsync(accessToken, cancellationToken);
    }
    
    public async ValueTask RemoveAccessTokenAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        await accessTokenRepository.DeleteByIdAsync(accessTokenId, cancellationToken);
    }

    public ValueTask RemoveRefreshTokenAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default)
    {
        return refreshTokenRepository.RemoveAsync(refreshTokenValue, cancellationToken);
    }
}