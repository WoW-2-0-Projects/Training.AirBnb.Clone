using System.Security.Authentication;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Constants;

namespace AirBnB.Api.Middlewares;

public class AccessTokenValidationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var identitySecurityTokenService = context.RequestServices.GetRequiredService<IIdentitySecurityTokenService>();

        var accessTokenIdValue = context.User.Claims.FirstOrDefault(claim => claim.Type == ClaimConstants.AccessTokenId)?.Value;
        if (accessTokenIdValue != null)
        {
            var accessTokenId = Guid.Parse(accessTokenIdValue);
            _ = await identitySecurityTokenService.GetAccessTokenByIdAsync(accessTokenId, context.RequestAborted) ??
                throw new AuthenticationException("Access token not found");
        }

        await next(context);
    }
}