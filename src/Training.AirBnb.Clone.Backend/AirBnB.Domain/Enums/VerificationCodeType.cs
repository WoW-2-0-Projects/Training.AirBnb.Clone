namespace AirBnB.Domain.Enums;

/// <summary>
/// Enum representing the type of verification code.
/// </summary>
public enum VerificationCodeType
{
    /// <summary>
    /// Verification code for email address verification.
    /// </summary>
    EmailAddressVerification,

    /// <summary>
    /// Verification code for phone number verification.
    /// </summary>
    PhoneNumberVerification,

    /// <summary>
    /// Verification code for account deletion verification.
    /// </summary>
    AccountDeleteVerification
}
