using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IMapper mapper,
    IUserService userService,
    IRoleService roleService,
    IAccountService accountService,
    IRoleProcessingService roleProcessingService,
    IAccessTokenService accessTokenService,
    IPasswordHasherService passwordHasherService,
    IPasswordGeneratorService passwordGeneratorService,
    IAccessTokenGeneratorService accessTokenGeneratorService
    ) : IAuthService
{
    
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        //check that the user is in the database at the entered email address
        var foundUserId = await userService.GetByEmailAddressAsync(signUpDetails.EmailAddress,true, cancellationToken);
        if (foundUserId is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        //map the entered user object
        var user = mapper.Map<User>(signUpDetails);
        
        //generating complex password
        var password = signUpDetails.AutoGeneratePassword
            ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, user);
        
        //hash password
        user.PasswordHash = passwordHasherService.HashPassword(password);
        
        return await accountService.CreateUserAsync(user, cancellationToken);
    }

    public async ValueTask<AccessToken> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        //bring the user all his materials from the database by email address entered
        var foundUser =
            await userService.Get(asNoTracking: true)
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.EmailAddress == signInDetails.EmailAddress,
                    cancellationToken: cancellationToken);
        
        //verify that the user has password entered correctly
        if(foundUser is null || passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
                throw new AuthenticationException("Sign in details are invalid, contact support.");
        
        //token generating 
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
        //Checking user having
        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("User not found");
        
        //authorization process 
        _ = await userService.GetByIdAsync(actionUserId, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Failed to retrieve user with ID {actionUserId}");
        
        //transfer of string type role to enam type
        if (!Enum.TryParse(roleType, out RoleType roleValue))  throw new InvalidOperationException("Invalid role type provided.");
        
        //get the desired role from the base
        var role = await roleService.GetByTypeAsync(roleValue, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Role with type '{roleValue}' could not be retrieved. It may not exist in the system");
        
        //change user role
        user.Roles = new List<Role>
        {
            role
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
        //authorization process 
        _ = await userService.GetByIdAsync(actionUserId, cancellationToken: cancellationToken) ?? throw new InvalidOperationException("Failed to retrieve user with ID {actionUserId}");
        
        //transfer of string type role to enam type
        if (!Enum.TryParse(roleType, out RoleType roleValue))  throw new InvalidOperationException("Invalid role type provided.");
        
        //check the level of the role
        if (roleValue >= actionUserRole || roleValue == RoleType.Guest)
            throw new AuthenticationException("Invalid role to update");
        
        await roleProcessingService.RevokeRoleAsync(userId, roleValue, cancellationToken);
        return true;
    }
}