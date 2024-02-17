using System.Security.Claims;
using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.RequestContexts.Brokers;

public class RequestUserContextProvider(
    IHttpContextAccessor httpContextAccessor, 
    IOptions<RequestUserContextSettings> userContextProvider)
    : IRequestUserContextProvider
{
    private readonly RequestUserContextSettings _requestUserContextSettings = userContextProvider.Value;

    public string? GetAccessToken()
    {
        return httpContextAccessor.HttpContext?.Request.Headers.Authorization;
    }

    public Guid GetUserId()
    {
        var httpContext = httpContextAccessor.HttpContext;

        var userIdClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.UserId)?.Value;

        return userIdClaim is not null ? Guid.Parse(userIdClaim) : _requestUserContextSettings.SystemUserId;
    }

    public RoleType GetUserRole()
    {
        var httpContext = httpContextAccessor.HttpContext;

        var roleClaim = httpContext!.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

        return roleClaim is not null ? Enum.Parse<RoleType>(roleClaim) : throw new InvalidOperationException("Role claim is missing");
    }
}