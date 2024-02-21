using Type = AirBnB.Domain.Enums.NotificationType;
namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents an SMS template for notifications, inheriting from NotificationTemplate.
/// </summary>
public class SmsTemplate : NotificationTemplate
{
    /// <summary>
    /// Initializes a new instance of the SmsTemplate class.
    /// </summary>
    /// <remarks>
    /// Sets the type of the template to Type.Sms.
    /// </remarks>
    public SmsTemplate() => Type = Type.Sms;
}