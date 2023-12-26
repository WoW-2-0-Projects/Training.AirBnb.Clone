namespace AirBnB.Domain.Enums;

/// <summary>
/// Represents the type of notification that can be used in the system.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Indicates an email notification type to send notifications via email.
    /// </summary>
    Email = 0,

    /// <summary>
    /// Indicates an SMS notification type to send notifications via SMS.
    /// </summary>
    Sms = 1,
}
