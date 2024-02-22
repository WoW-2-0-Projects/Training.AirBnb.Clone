using AirBnB.Application.Common.Identity.Queries;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Queries;
using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Twilio.Exceptions;

namespace AirBnB.Infrastructure.Common.Identity.QueryHandlers;

/// <summary>
/// Represents a query handler to get the current user's roles.
/// </summary>
public class GetCurrentUserRolesQueryHandler(
    IRequestUserContextProvider requestUserContextProvider,
    IIdentitySecurityTokenGeneratorService identitySecurityTokenGeneratorService,
    IUserService userService
) : IQueryHandler<GetCurrentUserRolesQuery, IList<Role>>
{
    public async Task<IList<Role>> Handle(GetCurrentUserRolesQuery request, CancellationToken cancellationToken)
    {
        var accessTokenValue = requestUserContextProvider.GetAccessToken()!;

        var accessToken = identitySecurityTokenGeneratorService.GetAccessToken(accessTokenValue);
        if (!accessToken.HasValue)
            throw new AuthenticationException("Invalid access token");

        var foundUser = await userService
            .Get(user => user.Id == accessToken.Value.AccessToken.UserId)
            .Include(user => user.Roles)
            .FirstAsync(cancellationToken: cancellationToken);

        return foundUser.Roles;
    }
}