using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;

/// <summary>
/// Represents an email history entry for notifications. 
/// Inherits from the base class NotificationHistory, providing additional properties specific to email notifications.
/// </summary>
public class EmailHistory : NotificationHistory
{
    /// <summary>
    /// Initializes a new instance of the EmailHistory class.
    /// Sets the notification type to Email upon creation.
    /// </summary>
    public EmailHistory()
    {
        Type = NotificationType.Email;
    }

    /// <summary>
    /// Gets or sets the email address of the sender.
    /// </summary>
    public string SenderEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the email address of the receiver.
    /// </summary>
    public string ReceiverEmailAddress { get; set; }

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets the associated EmailTemplate object for this email history entry.
    /// </summary>
    public EmailTemplate EmailTemplate { get; set; }
}
