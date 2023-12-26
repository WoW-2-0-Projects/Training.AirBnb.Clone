using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service responsible for retrieving roles based on the provided role type.
/// </summary>
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Constructor for RoleService initializing with an IRoleRepository.
    /// </summary>
    /// <param name="roleRepository">The repository handling role-related data operations.</param>
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    /// <summary>
    /// Retrieves a role based on the specified role type asynchronously.
    /// </summary>
    /// <param name="roleType">The type of role to retrieve.</param>
    /// <param name="asNoTracking">Flag indicating whether to use 'asNoTracking' while querying (default is false).</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>A ValueTask representing the retrieved role (or null if not found).</returns>
    public async ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        // Retrieve the role using the specified role type asynchronously
        return await _roleRepository.Get(asNoTracking: asNoTracking)
            .SingleOrDefaultAsync(role => role.Type == roleType, cancellationToken);
    }
}
