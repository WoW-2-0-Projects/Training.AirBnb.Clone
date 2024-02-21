using AirBnB.Domain.Enums;

namespace AirBnB.Application.Common.Verifications.Services;

/// <summary>
/// Interface for a service that handles verification codes.
/// </summary>
public interface IVerificationCodeService
{
    /// <summary>
    /// Gets the verification type associated with the given code asynchronously.
    /// </summary>
    /// <param name="code">The verification code to look up.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.
    /// The result is the <see cref="VerificationType"/> associated with the code,
    /// or <c>null</c> if the code is not found or has no associated type.
    /// </returns>
    ValueTask<VerificationType?> GetVerificationTypeAsync(string code, CancellationToken cancellationToken = default);
}