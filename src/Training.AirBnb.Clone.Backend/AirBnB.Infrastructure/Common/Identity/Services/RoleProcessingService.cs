using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Queries;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides services for managing user roles within the application.
/// </summary>
public class RoleProcessingService(
    IUserService userService,
    IRoleService roleService,
    IUserRoleRepository userRoleRepository
) : IRoleProcessingService
{
    public async ValueTask GrandRoleAsync(Guid userId, RoleType roleType, RoleType actionUserRole, CancellationToken cancellationToken = default)
    {
        // Query user
        var user = await userService.Get(queryOptions: new QueryOptions{AsNoTracking = true})
                       .Include(user => user.Roles)
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException("User not found");

        // Validate action permission
        if (actionUserRole <= RoleType.Host || actionUserRole <= roleType)
            throw new AuthenticationException("User does not have permission to grand a role");

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

    public async ValueTask GrandRoleBySystemAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default)
    {
        var user = await userService.Get(queryOptions: new QueryOptions{AsNoTracking = true})
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

    public async ValueTask RevokeRoleAsync(Guid userId, RoleType roleType, RoleType actionUserRole, CancellationToken cancellationToken = default)
    {
        var user = await userService.Get(queryOptions: new QueryOptions{AsNoTracking = true})
                       .Include(user => user.Roles)
                       .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException("User not found");

        //Choosing the right role
        var selectedRole = user.Roles.FirstOrDefault(role => role.Type == roleType) ??
                           throw new AuthenticationException("Given role wasn't granted to the user.");
        
        // Validate action permission
        if (actionUserRole <= RoleType.Host || actionUserRole <= roleType)
            throw new AuthenticationException("User does not have permission to grand a role");

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