using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// <summary>
/// Encapsulates authentication-related functionality, such as user registration, login, and role management.
/// </summary>
public class AuthService(
    IUserService userService,
    IRoleService roleService,
    IPasswordHasherService passwordHasherService,
    IUserRepository userRepository
    ) : IAuthService
{
    
    public async ValueTask<User> RegisterAsync(RegistrationDetails registrationDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = await userService.GetByEmailAddressAsync(registrationDetails.EmailAddress,true, cancellationToken);

        if (foundUser is not null)
            throw new InvalidOperationException("User with this email address already exists.");

        var defaultRole = await roleService.GetByTypeAsync(RoleType.Guest, true, cancellationToken) ??
                          throw new InvalidOperationException("Role with this type doesn't exist");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = registrationDetails.FirstName,
            LastName = registrationDetails.LastName,
            EmailAddress = registrationDetails.EmailAddress,
            PasswordHash = passwordHasherService.HashPassword(registrationDetails.Password),
            RoleId = defaultRole.Id
        };

        return await userRepository.CreateAsync(user, cancellationToken: cancellationToken);
    }

    public ValueTask<string> LoginAsync(LoginDetails loginDetails, CancellationToken cancellationToken = default)
    {
        var foundUser = userService
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