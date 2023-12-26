using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Verifications.Services;

/// <summary>
/// Interface for a service that handles user information verification codes.
/// </summary>
public interface IUserInfoVerificationCodeService : IVerificationCodeService
{
    /// <summary>
    /// Generates a list of user information verification codes.
    /// </summary>
    /// <returns>A list of generated codes.</returns>
    IList<string> Generate();

    /// <summary>
    /// Gets the user information verification code and its validity asynchronously based on the provided code.
    /// </summary>
    /// <param name="code">The verification code to look up.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result tuple contains the <see cref="UserInfoVerificationCode"/> and a boolean indicating whether the code is valid.
    /// </returns>
    ValueTask<(UserInfoVerificationCode Code, bool IsValid)> GetByCodeAsync(string code, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new user information verification code asynchronously.
    /// </summary>
    /// <param name="codeType">The type of verification code to create.</param>
    /// <param name="userId">The unique identifier of the user associated with the code.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The result is the created <see cref="UserInfoVerificationCode"/>.</returns>
    ValueTask<UserInfoVerificationCode> CreateAsync(VerificationCodeType codeType, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates user information verification codes associated with the specified user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="saveChanges">Indicates whether to save changes to the underlying storage.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask DeactivateAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);
}