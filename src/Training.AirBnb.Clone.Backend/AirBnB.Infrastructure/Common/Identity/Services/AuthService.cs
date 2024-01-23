using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IUserService userService,
    IMapper mapper,
    IAccountService accountService,
    IPasswordGeneratorService passwordGeneratorService,
    IPasswordHasherService passwordHasherService,
    IAccessTokenService accessTokenService,
    IAccessTokenGeneratorService accessTokenGeneratorService
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
            await userService.GetByEmailAddressAsync(signInDetails.EmailAddress, true, cancellationToken);
        
        if(foundUser is null || passwordHasherService.ValidatePassword(signInDetails.Password, foundUser.PasswordHash))
                throw new AuthenticationException("SignUp details are invalid, contact support.");

        var accessToken = accessTokenGeneratorService.GetToken(foundUser);
        await accessTokenService.CreateAsync(accessToken, true, cancellationToken);

        return accessToken;
    }
    

    public ValueTask<bool> GrandRoleAsync(Guid userId, string roleType, Guid actionUserId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> RevokeRoleAsync(Guid userId, string roleType, Guid actionUserId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}