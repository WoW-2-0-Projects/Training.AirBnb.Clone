using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides services for managing user roles within the application.
/// </summary>
/// <param name="appDbContext"></param>
/// <param name="userService"></param>
public class RoleProcessingService(
    AppDbContext appDbContext,
    IUserService userService,
    IRoleService roleService,
    IUserRoleRepository userRoleRepository
) : IRoleProcessingService
{
    public async ValueTask GrandRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default)
    {
        // Query user
        var user = await userService.Get(asNoTracking: true)
                       .Include(user => user.Roles)
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException("User not found");

        if (user.Roles.Any(role => role.Type == roleType))
            throw new InvalidOperationException("User already has the role");

        // Query selected role
        var selectedRole = await roleService.GetByTypeAsync(roleType, cancellationToken: cancellationToken) ??
                           throw new InvalidOperationException("Role not found");

        // Add role to user
        await userRoleRepository.CreateAsync(
            new UserRole
            {
                RoleId = selectedRole.Id,
                UserId = user.Id
            },
            cancellationToken: cancellationToken
        );
    }

    public async ValueTask RevokeRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default)
    {
        // TODO : Add navigation to user role to role and use then include
        var user = await userService.Get(asNoTracking: true)
                       .Include(user => user.Roles)
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException("User not found");

        //Choosing the right role
        var selectedRole = user.Roles.FirstOrDefault(role => role.Type == roleType) ??
                           throw new AuthenticationException("Given role wasn't grand to the user.");

        // Delete the selected role
        await userRoleRepository.DeleteAsync(
            new UserRole
            {
                RoleId = selectedRole.Id,
                UserId = user.Id
            },
            cancellationToken: cancellationToken
        );
    }
}