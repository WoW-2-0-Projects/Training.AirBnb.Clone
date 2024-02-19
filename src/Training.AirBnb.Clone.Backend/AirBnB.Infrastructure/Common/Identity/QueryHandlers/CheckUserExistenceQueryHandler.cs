using AirBnB.Application.Common.Identity.Queries;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.QueryHandlers;

/// <summary>
/// Provides a handler for the check user by email address query
/// </summary>
public class CheckUserExistenceQueryHandler(IUserService userService)
    : IQueryHandler<CheckUserByEmailAddressQuery, bool>, IQueryHandler<CheckUserByPhoneNumberQuery, bool>
{
    public Task<bool> Handle(CheckUserByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        var userExists = userService.Get(
                user => user.EmailAddress == request.EmailAddress,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .AnyAsync(cancellationToken: cancellationToken);

        return userExists;
    }

    public Task<bool> Handle(CheckUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var userExists = userService.Get(
                user => user.PhoneNumber == request.PhoneNumber,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .AnyAsync(cancellationToken: cancellationToken);

        return userExists;
    }
}