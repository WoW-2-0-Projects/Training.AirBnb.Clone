using AirBnB.Application.Common.Identity.Models;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

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
    ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously attempts to log in a user with the provided credentials.
    /// </summary>
    /// <param name="signInDetails">The login details containing the user's username and password.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the login operation.</param>
    /// <returns>A ValueTask<string> that contains:
    /// - An authentication token if the login is successful.
    /// - Null if the login fails.</returns>
    ValueTask<AccessToken> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously grants a specified role to a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user to receive the role.</param>
    /// <param name="roleType">The type of role to grant (e.g., "Admin", "User", "Moderator", etc.).</param>
    /// <param name="actionUserId">he unique identifier of the user performing the role granting action.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>A ValueTask<bool> indicating whether the role was granted successfully.</returns>
    ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, Guid actionUserId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously revokes a specified role from a user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user from whom to revoke the role.</param>
    /// <param name="roleType">The type of role to revoke (e.g., "Admin", "User", "Moderator", etc.).</param>
    /// <param name="actionUserId">The unique identifier of the user performing the role revocation action.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns></returns>
    ValueTask<bool> RevokeRoleAsync(Guid userId, string roleType, Guid actionUserId, RoleType actionUserRole, CancellationToken cancellationToken = default);
}