using AirBnB.Application.Common.Identity.Commands;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Commands;
using AirBnB.Domain.Entities;
using Twilio.Exceptions;

namespace AirBnB.Infrastructure.Common.Identity.CommandHandlers;

/// <summary>
/// Sign in command handler
/// </summary>
public class SignInCommandHandler(IAuthService authService) : ICommandHandler<SignInCommand, (AccessToken accessToken, RefreshToken refreshToken)>
{
    public async Task<(AccessToken accessToken, RefreshToken refreshToken)> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        if (request.SignInByEmail is not null)
            return await authService.SignInByEmailAsync(request.SignInByEmail, cancellationToken);

        if (request.SignInByPhone is not null)
            return await authService.SignInByPhoneAsync(request.SignInByPhone, cancellationToken);

        throw new AuthenticationException("Invalid sign in request");
    }
}