using AirBnB.Domain.Common;
using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a verification code entity.
/// </summary>
public abstract class VerificationCode : Entity
{
    /// <summary>
    /// Gets or sets the type of the verification code.
    /// </summary>
    public VerificationCodeType CodeType { get; set; }

    /// <summary>
    /// Gets or sets the type of verification.
    /// </summary>
    public VerificationType Type { get; set; }

    /// <summary>
    /// Gets or sets the expiration time of the verification code.
    /// </summary>
    public DateTimeOffset ExpiryTime { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the verification code is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets or sets the actual verification code.
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Gets or sets the verification link associated with the code.
    /// </summary>
    public string VerificationLink { get; set; } = default!;
}