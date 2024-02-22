using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Queries;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Queries;
using AirBnB.Domain.Entities;

namespace AirBnB.Infrastructure.Common.Identity.QueryHandlers;

public class GetCurrentUserQueryHandler(IRequestUserContextProvider requestUserContextProvider, IUserService userService) : IQueryHandler<GetCurrentUserQuery, User>
{
    public async Task<User> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = requestUserContextProvider.GetUserId();

        var foundUser = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken);
        if(foundUser is null)
            throw new AuthenticationException("Current logged in user not found");
        
        return foundUser;
    }
}