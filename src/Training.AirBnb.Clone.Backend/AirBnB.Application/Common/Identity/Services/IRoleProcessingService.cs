using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Identity.Services;

public interface IRoleProcessingService
{
    /// <summary>
    /// Grands a specified role to a user asynchronously.
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="roleType">Type of role to grand.</param>
    /// <param name="cancellationToken">Cancellation token</param>
    ValueTask GrandRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Revokes a specified role from a user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose role is being revoked</param>
    /// <param name="roleType">The type of role to revoke.</param>
    /// <param name="cancellationToken"> A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that will complete with a value indicating whether the role was successfully revoked</returns>
    ValueTask RevokeRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default);
}