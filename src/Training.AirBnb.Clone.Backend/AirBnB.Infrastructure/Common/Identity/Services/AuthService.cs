using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.DataContexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IUserService userService,
    IMapper mapper,
    IRoleService roleService,
    IAccountService accountService,
    IPasswordGeneratorService passwordGeneratorService,
    IPasswordHasherService passwordHasherService,
    IAccessTokenService accessTokenService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    AppDbContext appDbContext
    ) : IAuthService
{
    
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var foundUserId = await userService.GetByEmailAddressAsync(signUpDetails.EmailAddress,true, cancellationToken);

        if (foundUserId is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        var user = mapper.Map<User>(signUpDetails);
        var password = signUpDetails.AutoGeneratePassword
            ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, user);
        
        user.PasswordHash = passwordHasherService.HashPassword(password);
        
        return await accountService.CreateUserAsync(user, cancellationToken);
    }

    public async ValueTask<AccessToken> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        var foundUser =
            await userService.Get(asNoTracking: true)
                .Include(user => user.Roles)
                .ThenInclude(role => role.Role)
                .FirstOrDefaultAsync(user => user.EmailAddress == signInDetails.EmailAddress,
                    cancellationToken: cancellationToken);
        
        if(foundUser is null || passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
                throw new AuthenticationException("Sign in details are invalid, contact support.");

        var accessToken = accessTokenGeneratorService.GetToken(foundUser);
        await accessTokenService.CreateAsync(accessToken, true, cancellationToken);

        return accessToken;
    }

    public async ValueTask<bool> GrandRoleAsync(
        Guid userId, 
        string roleType, 
        Guid actionUserId,
        CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("User not found");
        _ = await userService.GetByIdAsync(actionUserId, cancellationToken: cancellationToken) ?? throw new IncompleteInitialization();

        if (!Enum.TryParse(roleType, out RoleType roleValue))  throw new InvalidOperationException("Invalid role type provided.");
        
        var role = await roleService.GetByTypeAsync(roleValue, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Role with type '{roleValue}' could not be retrieved. It may not exist in the system");
        
        user.Roles = new List<UserRole>
        {
            new UserRole
            {
                RoleId = role.Id
            }
        };
        
        await userService.UpdateAsync(user, cancellationToken: cancellationToken);

        return true;
    }

    public async ValueTask<bool> RevokeRoleAsync(
        Guid userId, 
        string roleType, 
        Guid actionUserId,
        RoleType actionUserRole,
        CancellationToken cancellationToken = default)
    {
        var user = await userService.Get(asNoTracking: true)
            .Include(user => user.Roles)
            .ThenInclude(role => role.Role)
            .FirstOrDefaultAsync(user => user.Id == userId,
                cancellationToken: cancellationToken) ?? throw new InvalidOperationException("User not found");
        
        _ = await userService.GetByIdAsync(actionUserId, cancellationToken: cancellationToken) ?? throw new IncompleteInitialization();
        
        if (!Enum.TryParse(roleType, out RoleType roleValue))  throw new InvalidOperationException("Invalid role type provided.");

        if (roleValue >= actionUserRole || roleValue == RoleType.Guest )
            throw new AuthenticationException("Invalid  role to update");

        var selectedRole = user.Roles.FirstOrDefault(role => role.Role.Type == roleValue) ?? throw new AuthenticationException(" invalid role");

        //user.Roles.Remove(selectedRole);

        appDbContext.UserRoles.Remove(selectedRole);
        await appDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}