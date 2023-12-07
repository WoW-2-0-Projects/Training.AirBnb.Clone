using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Infrastructure.Common.Identity.Services;

public class AccountService(IUserService userService,IUserRepository userRepository) : IAccountService
{
    public ValueTask<bool> CreateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<User> GetUserByEmailAddressAsync(string emailAddress, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        /*return await userRepository.Get(asNoTracking:asNoTracking)
            .SingleOrDefaultAsync(userEmail => userEmail.EmailAddress == emailAddress, cancellationToken);*/
        throw new NotImplementedException();
    }

    public ValueTask<bool> VerificateUserAsync(string token, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}