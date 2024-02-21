using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Identity.Services;

/// <summary>
/// Represents a service for managing user accounts.
/// </summary>
public interface IAccountService
{
    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="emailAddress">The email address of the user to retrieve.</param>
    /// <param name="asNoTracking">Indicates whether to disable change tracking. Default is false.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>A <see cref="ValueTask"/> containing the retrieved user or null if not found.</returns>
    ValueTask<User?> GetUserByEmailAddressAsync(string emailAddress, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The user object to be created.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>True if the user is created successfully; otherwise, false.</returns>
    ValueTask<User> CreateUserAsync(User user, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verifies a user using a verification code.
    /// </summary>
    /// <param name="code">The verification code associated with the user.</param>
    /// <param name="cancellationToken">Cancellation token for asynchronous operations.</param>
    /// <returns>True if the user is successfully verified; otherwise, false.</returns>
    ValueTask<bool> VerifyUserAsync(string code, CancellationToken cancellationToken = default);
}
