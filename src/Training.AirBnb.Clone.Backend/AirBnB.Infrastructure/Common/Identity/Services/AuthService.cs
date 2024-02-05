using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Domain.Extensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IMapper mapper,
    IUserService userService,
    IAccountService accountService,
    IRoleProcessingService roleProcessingService,
    IAccessTokenService accessTokenService,
    IPasswordHasherService passwordHasherService,
    IPasswordGeneratorService passwordGeneratorService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IRequestUserContextProvider requestUserContextProvider
) : IAuthService
{
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        //Check that the user is in the database at the entered email address
        var foundUserId = await userService.GetByEmailAddressAsync(signUpDetails.EmailAddress, true, cancellationToken);
        if (foundUserId is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        //Map the entered user object
        var user = mapper.Map<User>(signUpDetails);

        //Generating complex password
        var password = signUpDetails.AutoGeneratePassword
            ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, user);

        //Hash password
        user.PasswordHash = passwordHasherService.HashPassword(password);

        var createdUser = await accountService.CreateUserAsync(user, cancellationToken);

        // Grand guest role
        await roleProcessingService.GrandRoleBySystemAsync(createdUser.Id, RoleType.Guest, cancellationToken);

        // TODO : add other validation logic
        return createdUser is not null;
    }

    public async ValueTask<AccessToken> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        //Bring the user all his materials from the database by email address entered
        var foundUser = await userService.Get(asNoTracking: true)
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.EmailAddress == signInDetails.EmailAddress, cancellationToken: cancellationToken);

        //Verify that the user has password entered correctly
        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
            throw new AuthenticationException("Sign in details are invalid, contact support.");

        //Token generating 
        var accessToken = accessTokenGeneratorService.GetToken(foundUser);
        await accessTokenService.CreateAsync(accessToken, true, cancellationToken);

        return accessToken;
    }

    public async ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse(roleType, out RoleType roleValue))
            throw new InvalidOperationException("Invalid role type provided.");

        var grandRoleTask = () => roleProcessingService.GrandRoleAsync(
            userId,
            roleValue,
            requestUserContextProvider.GetUserRole(),
            cancellationToken
        );
        var grandRoleValue = await grandRoleTask.GetValueAsync();

        if (grandRoleValue is { IsSuccess: false, Exception: not null })
            throw grandRoleValue.Exception;

        // TODO : Send role granted notification

        return true;
    }

    public async ValueTask<bool> RevokeRoleAsync(Guid userId, string roleType, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse(roleType, out RoleType roleValue))
            throw new InvalidOperationException("Invalid role type provided.");

        var revokeRoleTask = () => roleProcessingService.RevokeRoleAsync(
            userId,
            roleValue,
            requestUserContextProvider.GetUserRole(),
            cancellationToken
        );
        var grandRoleValue = await revokeRoleTask.GetValueAsync();

        if (grandRoleValue is { IsSuccess: false, Exception: not null })
            throw grandRoleValue.Exception;

        // TODO : Send role revoked notification

        return true;
    }
}