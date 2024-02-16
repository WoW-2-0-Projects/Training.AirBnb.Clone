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
    IIdentitySecurityTokenService identitySecurityTokenService,
    IPasswordHasherService passwordHasherService,
    IPasswordGeneratorService passwordGeneratorService,
    IIdentitySecurityTokenGeneratorService identitySecurityTokenGeneratorService,
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
        user.UserCredentials = new UserCredentials
        {
            PasswordHash = passwordHasherService.HashPassword(password)
        };

        var createdUser = await accountService.CreateUserAsync(user, cancellationToken);

        // Grand guest role
        await roleProcessingService.GrandRoleBySystemAsync(createdUser.Id, RoleType.Guest, cancellationToken);

        // TODO : add other validation logic
        return createdUser is not null;
    }

    public async ValueTask<(AccessToken accessToken, RefreshToken refreshToken)> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken)
    {
        var foundUser = await userService.GetByEmailAddressAsync(signInDetails.EmailAddress, cancellationToken: cancellationToken);

        if (foundUser is null || !passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.UserCredentials.PasswordHash))
            throw new AuthenticationException("Sign in details are invalid, contact support.");

        if (!foundUser.IsEmailAddressVerified)
            throw new AuthenticationException("Email address is not verified.");

        return await CreateTokens(foundUser, cancellationToken);
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

    public async ValueTask<AccessToken> RefreshTokenAsync(string refreshTokenValue, CancellationToken cancellationToken = default)
    {
        var accessTokenValue = requestUserContextProvider.GetAccessToken();

        if (string.IsNullOrWhiteSpace(refreshTokenValue))
            throw new ArgumentException("Invalid identity security token value", nameof(refreshTokenValue));

        if (string.IsNullOrWhiteSpace(accessTokenValue))
            throw new InvalidOperationException("Invalid identity security token value");

        // Check refresh token and access token
        var refreshToken = await identitySecurityTokenService.GetRefreshTokenByValueAsync(refreshTokenValue, cancellationToken);
        if (refreshToken is null)
            throw new AuthenticationException("Please login again.");

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (!accessToken.HasValue)
        {
            // Remove refresh token if access token is not valid
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            throw new InvalidOperationException("Invalid identity security token value");
        }
        
        var foundAccessToken = await identitySecurityTokenService.GetAccessTokenByIdAsync(accessToken.Value.AccessToken.Id, cancellationToken);

        // Remove refresh token and access token if user id is not same
        if (refreshToken.UserId != accessToken.Value.AccessToken.UserId)
        {
            await identitySecurityTokenService.RemoveRefreshTokenAsync(refreshTokenValue, cancellationToken);
            if(foundAccessToken is not null)
                await identitySecurityTokenService.RevokeAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
            
            throw new AuthenticationException("Please login again.");
        }

        var foundUser =
            await userService
                .Get(user => user.Id == accessToken.Value.AccessToken.UserId, true)
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken) ??
            throw new InvalidOperationException();

        // If access token exists, not revoked and still valid return it, otherwise remove
        if (foundAccessToken is not null && !foundAccessToken.IsRevoked)
        {
            if(!foundAccessToken.IsRevoked)
                return foundAccessToken;
            await identitySecurityTokenService.RemoveAccessTokenAsync(accessToken.Value.AccessToken.Id, cancellationToken);
        }
        
        // Generate access token
        var newAccessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(foundUser);

        return await identitySecurityTokenService.CreateAccessTokenAsync(newAccessToken, cancellationToken: cancellationToken);
    }

    private async Task<(AccessToken AccessToken, RefreshToken RefreshToken)> CreateTokens(User user, CancellationToken cancellationToken = default)
    {
        var accessToken = identitySecurityTokenGeneratorService.GenerateAccessToken(user);

        var refreshToken = identitySecurityTokenGeneratorService.GenerateRefreshToken(user);

        return (await identitySecurityTokenService.CreateAccessTokenAsync(accessToken, cancellationToken: cancellationToken),
            await identitySecurityTokenService.CreateRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken));
    }
}