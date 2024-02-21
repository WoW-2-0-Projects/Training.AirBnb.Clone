namespace AirBnB.Domain.Constants;

/// <summary>
/// Constants for placeholders used in notification templates.
/// </summary>
public static class NotificationTemplateConstants
{
    /// <summary>
    /// Placeholder for the user's name in notification templates.
    /// </summary>
    public const string UserNamePlaceholder = "UserName";

    /// <summary>
    /// Placeholder for the sender's name in notification templates.
    /// </summary>
    public const string SenderNamePlaceholder = "SenderName";
    
    /// <summary>
    /// Placeholder for the verification link in email address verification notification templates.
    /// </summary>
    public const string EmailAddressVerificationCodePlaceholder = "EmailAddressVerificationCode";
}