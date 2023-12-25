using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace AirBnB.Application.Common.Identity.Services
{
    /// <summary>
    /// Interface defining operations related to roles in the application.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Retrieves a role based on the specified role type asynchronously.
        /// </summary>
        /// <param name="roleType">The type of role to retrieve.</param>
        /// <param name="asNoTracking">Flag indicating whether to use 'asNoTracking' while querying (default is false).</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>A ValueTask representing the retrieved role (or null if not found).</returns>
        ValueTask<Role?> GetByTypeAsync(
            RoleType roleType,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default
        );
    }
}
