using AirBnB.Application.Common.Identity.Commands;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Commands;
using AirBnB.Domain.Extension;

namespace AirBnB.Infrastructure.Common.Identity.CommandHandlers;

/// <summary>
/// Represents a command handler to sign out a user.
/// </summary>
public class SignOutCommandHandler(IRequestUserContextProvider requestUserContextProvider, IAuthService authService) : ICommandHandler<SignOutCommand, bool>
{
    public async Task<bool> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        var accessToken = requestUserContextProvider.GetAccessToken()!;
        var signOutTask = () => authService.SignOutAsync(accessToken, request.RefreshToken, cancellationToken);
        var signOutResult = await signOutTask.GetValueAsync();

        return signOutResult.IsSuccess;
    }
}