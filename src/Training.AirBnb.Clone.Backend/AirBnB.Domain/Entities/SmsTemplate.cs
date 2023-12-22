using Type = AirBnB.Domain.Enums.NotificationType;
namespace AirBnB.Domain.Entities;

/// <summary>
/// SmsTemplate model implemented NotificationTemplate abstract model
/// </summary>
public class SmsTemplate : NotificationTemplate
{
    /// <summary>
    /// set default Type property
    /// </summary>
    public SmsTemplate() => Type = Type.Sms;
}