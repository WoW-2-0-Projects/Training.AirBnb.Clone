namespace AirBnB.Domain.Enums;

/// <summary>
/// Enumerates the types of notification templates.
/// </summary>
public enum NotificationTemplateType
{
    /// <summary>
    /// Represents a system welcome notification template.
    /// </summary>
    SystemWelcomeNotification = 0,

    /// <summary>
    /// Represents an email verification notification template.
    /// </summary>
    EmailVerificationNotification = 1,

    /// <summary>
    /// Represents a referral notification template.
    /// </summary>
    ReferralNotification = 2,

    /// <summary>
    /// Represents a phone number verification notification template.
    /// </summary>
    PhoneNumberVerificationNotification = 3
}
