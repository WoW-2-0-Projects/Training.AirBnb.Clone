using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Service responsible for retrieving roles based on the provided role type.
/// </summary>
public class RoleService(IRoleRepository roleRepository) : IRoleService
{
    public async ValueTask<Role?> GetByTypeAsync(RoleType roleType, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await roleRepository.Get(asNoTracking: asNoTracking)
            .FirstOrDefaultAsync(role => role.Type == roleType, cancellationToken);
    }
}
