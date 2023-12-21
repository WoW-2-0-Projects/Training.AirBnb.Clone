using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Validators;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.Services;

public class AccountService(UserValidator validator, IUserRepository userRepository) : IAccountService
{
    public async ValueTask<bool> CreateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult =
             validator.Validate(user, options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await userRepository.CreateAsync(user, saveChanges, cancellationToken); 

        return true;
    }

    public async ValueTask<User?> GetUserByEmailAddressAsync(string emailAddress, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        if (emailAddress is null)
            throw new ArgumentNullException(nameof(emailAddress), "Email address not found!");

        var query = userRepository.Get(user => user.EmailAddress == emailAddress);

        if (asNoTracking)
            query = query.AsNoTracking();

        var account = await query.SingleOrDefaultAsync(cancellationToken);

        return account;
    }

    public ValueTask<bool> VerificateUserAsync(string token, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new Exception();
    }
}