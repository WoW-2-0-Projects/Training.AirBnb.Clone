using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

/// <summary>
/// Represents an SMS (Short Message Service) history entry for notifications.
/// Inherits from the base class NotificationHistory, providing additional properties specific to SMS notifications.
/// </summary>
public class SmsHistory : NotificationHistory
{
    /// <summary>
    /// Initializes a new instance of the SmsHistory class.
    /// Sets the notification type to Sms upon creation.
    /// </summary>
    public SmsHistory()
    {
        Type = NotificationType.Sms;
    }

    /// <summary>
    /// Gets or sets the phone number of the sender.
    /// </summary>
    public string SenderPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the phone number of the receiver.
    /// </summary>
    public string ReceiverPhoneNumber { get; set; }

    /// <summary>
    /// Gets or sets the associated SmsTemplate object for this SMS history entry.
    /// </summary>
    public SmsTemplate Template { get; set; }
}