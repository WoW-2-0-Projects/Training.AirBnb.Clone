using AirBnB.Domain.Entities;

namespace AirBnB.Application.Common.Notifications.Models;

/// <summary>
/// Represents an sms message for notifications
/// </summary>
public class SmsMessage : NotificationMessage
{
    /// <summary>
    /// Gets or sets sms phone Number of sender user
    /// </summary>
    public string SenderPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets sms phone Number of receiver user
    /// </summary>
    public string ReceiverPhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets sms template of the sms message
    /// </summary>
    public SmsTemplate Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets message of the sms message
    /// </summary>
    public string Message { get; set; } = default!;
}
