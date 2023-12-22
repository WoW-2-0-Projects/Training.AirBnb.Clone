using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents a verification code specifically for user information.
/// </summary>
public class UserInfoVerificationCode : VerificationCode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInfoVerificationCode"/> class.
    /// </summary>
    public UserInfoVerificationCode()
    {
        Type = VerificationType.UserInfoVerificationCode;
    }

    /// <summary>
    /// Gets or sets the unique identifier of the user associated with the verification code.
    /// </summary>
    public Guid UserId { get; set; }
}
