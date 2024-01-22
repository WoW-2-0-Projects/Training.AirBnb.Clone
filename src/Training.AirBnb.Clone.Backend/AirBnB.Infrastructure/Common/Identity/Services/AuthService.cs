using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;
using AutoMapper;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IUserService userService,
    IMapper mapper,
    IAccountService accountService,
    IPasswordGeneratorService passwordGeneratorService,
    IPasswordHasherService passwordHasherService,
    IRoleService roleService,
    IUserRepository userRepository
    ) : IAuthService
{
    
    public async ValueTask<bool> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        var foundUserId = await userService.GetByEmailAddressAsync(signUpDetails.EmailAddress,true, cancellationToken);

        if (foundUserId is null)
            throw new InvalidOperationException("User with this email address already exists.");

        var user = mapper.Map<User>(signUpDetails);
        var password = signUpDetails.AutoGeneratePassword
            ? passwordGeneratorService.GeneratePassword()
            : passwordGeneratorService.GetValidatedPassword(signUpDetails.Password!, user);
        
        user.PasswordHash = passwordHasherService.HashPassword(password);
        
        return await accountService.CreateUserAsync(user, cancellationToken);
    }

    public ValueTask<string> SignUpAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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