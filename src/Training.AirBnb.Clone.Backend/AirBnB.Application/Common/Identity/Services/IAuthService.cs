﻿using AirBnB.Application.Common.Identity.Models;
using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Services;

/// <summary>
/// Service interface for authorization operations
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Asynchronously registers a new user with the specified registration details.
    /// </summary>
    /// <param name="signUpDetails">The registration information for the new user</param>
    /// <param name="cancellationToken">A token that can be used to cancel the registration operation</param>
    /// <returns>A ValueTask<bool> that indicates whether the registration was successful.</returns>
    ValueTask<bool> SignUpAsync(
        SignUpDetails signUpDetails, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Signs in user by email
    /// </summary>
    /// <param name="signInDetails">Sign in credentials</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Identity security tokens</returns>
    ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInByEmailAsync(
        SignInByEmailDetails signInDetails, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Signs in user by phone
    /// </summary>
    /// <param name="signInDetails">Sign in credentials</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Identity security tokens</returns>
    ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInByPhoneAsync(
        SignInByPhoneDetails signInDetails, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously grants a specified role to a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to receive the role.</param>
    /// <param name="roleType">The type of role to grant (e.g., "Admin", "User", "Moderator", etc.).</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A ValueTask<bool> indicating whether the role was granted successfully.</returns>
    ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously revokes a specified role from a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user from whom to revoke the role.</param>
    /// <param name="roleType">The type of role to revoke (e.g., "Admin", "User", "Moderator", etc.).</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns></returns>
    ValueTask<bool> RevokeRoleAsync(Guid userId, string roleType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks given refresh token and access token, then generates new access token for user
    /// </summary>
    /// <param name="refreshTokenValue">The unique token value of the refresh token to get</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation if needed</param>
    /// <returns></returns>
    ValueTask<AccessToken> RefreshTokenAsync(
        string refreshTokenValue, 
        CancellationToken cancellationToken = default);
}