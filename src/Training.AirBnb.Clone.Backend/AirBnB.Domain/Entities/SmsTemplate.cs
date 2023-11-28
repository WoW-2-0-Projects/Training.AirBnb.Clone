using Type = AirBnB.Domain.Enums.NotificationType;
namespace AirBnB.Domain.Entities;

/// <summary>
/// SmsTemplate model implented NotificationTemplate abstract model
/// </summary>
public class SmsTemplate : NotificationTemplate
{
    /// <summary>
    /// set default Type prperty
    /// </summary>
    public SmsTemplate() => Type = Type.Sms;
}