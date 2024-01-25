using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides services for managing user roles within the application.
/// </summary>
/// <param name="appDbContext"></param>
/// <param name="userService"></param>
public class UserRoleService(
    AppDbContext appDbContext, 
    IUserService userService
    ) : IUserRoleService
{
    public async ValueTask<bool> RevokeRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default)
    {
        //bring the user all his materials from the database by email address entered
        var user = await userService.Get(asNoTracking: true)
            .Include(user => user.Roles)
            .ThenInclude(role => role.Role)
            .FirstOrDefaultAsync(user => user.Id == userId,
                cancellationToken: cancellationToken) ?? throw new InvalidOperationException("User not found");
        
        //choosing the right role
        var selectedRole = user.Roles.FirstOrDefault(role => role.Role.Type == roleType) ?? throw new AuthenticationException(" Invalid role type");
        
        //delete the selected role
        appDbContext.UserRoles.Remove(selectedRole);
        
        await appDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}