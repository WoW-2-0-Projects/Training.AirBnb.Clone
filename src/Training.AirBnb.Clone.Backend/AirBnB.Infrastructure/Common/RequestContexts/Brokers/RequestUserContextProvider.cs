using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.RequestContexts.Brokers;

public class RequestUserContextProvider(
    IHttpContextAccessor httpContextAccessor,
    IOptions<RequestUserContextSettings> userContextProvider) 
    : IRequestUserContextProvider
{
    private readonly RequestUserContextSettings _requestUserContextSettings = userContextProvider.Value;
    
    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;
        
        var userIdClaim = httpContext!.User.Claims
            .FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)?.Value;

        return userIdClaim is not null ? Guid.Parse(userIdClaim) : _requestUserContextSettings.SystemUserId;
    }
}