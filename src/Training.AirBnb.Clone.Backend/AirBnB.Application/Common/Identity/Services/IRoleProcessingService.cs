using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Identity.Services;

public interface IRoleProcessingService
{
    /// <summary>
    /// Revokes a specified role from a user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose role is being revoked</param>
    /// <param name="roleType">The type of role to revoke.</param>
    /// <param name="cancellationToken"> A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that will complete with a value indicating whether the role was successfully revoked</returns>
    ValueTask<bool> RevokeRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default);
}