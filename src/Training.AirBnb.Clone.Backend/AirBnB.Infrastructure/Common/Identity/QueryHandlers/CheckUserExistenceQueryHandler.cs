using AirBnB.Application.Common.Identity.Queries;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Common.Identity.QueryHandlers;

/// <summary>
/// Provides a handler for the check user by email address query
/// </summary>
public class CheckUserExistenceQueryHandler(IUserService userService)
    : IQueryHandler<CheckUserByEmailAddressQuery, string?>, IQueryHandler<CheckUserByPhoneNumberQuery, string?>
{
    public Task<string?> Handle(CheckUserByEmailAddressQuery request, CancellationToken cancellationToken)
    {
        var userFirstname =  userService
            .Get(
                user => user.EmailAddress == request.EmailAddress,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .Select(user => user.FirstName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return userFirstname;
    }

    public Task<string?> Handle(CheckUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var userFirstname =  userService
            .Get(
                user => user.PhoneNumber == request.PhoneNumber,
                new QueryOptions
                {
                    AsNoTracking = true
                }
            )
            .Select(user => user.FirstName)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        return userFirstname;
    }
}