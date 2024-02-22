using AirBnB.Domain.Common.Commands;

namespace AirBnB.Application.Common.Identity.Commands;

/// <summary>
/// Represents a command to sign out a user.
/// </summary>
public class SignOutCommand : ICommand<bool>
{
    /// <summary>
    /// User's refresh token
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}
