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
public class RoleProcessingService(
    AppDbContext appDbContext, 
    IUserService userService
    ) : IRoleProcessingService
{
    public async ValueTask<bool> RevokeRoleAsync(Guid userId, RoleType roleType, CancellationToken cancellationToken = default)
    {
        //Bring the user all his materials from the database by email address entered
        var user = await userService.Get(asNoTracking: true)
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == userId,
                cancellationToken: cancellationToken) ?? throw new InvalidOperationException("User not found");
        
        //Choosing the right role
        var selectedRole = user.Roles.FirstOrDefault(role => role.Type == roleType) ?? throw new AuthenticationException(" Invalid role type");
        
        //Delete the selected role
        appDbContext.Roles.Remove(selectedRole);
        
        await appDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}