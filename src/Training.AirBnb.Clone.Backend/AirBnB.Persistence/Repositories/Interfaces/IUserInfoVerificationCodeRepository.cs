using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

/// <summary>
/// Interface for a repository that manages user information verification codes.
/// </summary>
public interface IUserInfoVerificationCodeRepository
{
    /// <summary>
    /// Gets a queryable collection of user information verification codes based on the optional predicate.
    /// </summary>
    /// <param name="predicate">An optional predicate to filter the codes.</param>
    /// <param name="asNoTracking">Indicates whether to enable tracking for the retrieved codes.</param>
    /// <returns>A queryable collection of user information verification codes.</returns>
    IQueryable<UserInfoVerificationCode> Get(Expression<Func<UserInfoVerificationCode, bool>>? predicate = default,
        bool asNoTracking = false);

    /// <summary>
    /// Gets a user information verification code by its unique identifier asynchronously.
    /// </summary>
    /// <param name="codeId">The unique identifier of the verification code.</param>
    /// <param name="asNoTracking">Indicates whether to enable tracking for the retrieved code.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result is the user information verification code, or <c>null</c> if not found.
    /// </returns>
    ValueTask<UserInfoVerificationCode?> GetByIdAsync(Guid codeId, bool asNoTracking = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user information verification code asynchronously.
    /// </summary>
    /// <param name="verificationCode">The user information verification code to create.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the underlying storage.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result is the created user information verification code.
    /// </returns>
    ValueTask<UserInfoVerificationCode> CreateAsync(UserInfoVerificationCode verificationCode, bool saveChanges = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates a user information verification code by its unique identifier asynchronously.
    /// </summary>
    /// <param name="codeId">The unique identifier of the verification code to deactivate.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the underlying storage.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask"/> representing the asynchronous operation.
    /// </returns>
    ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default);
}