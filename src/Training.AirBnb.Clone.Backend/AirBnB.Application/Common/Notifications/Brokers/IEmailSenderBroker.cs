using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Brokers;
/// <summary>
/// Interface for an email sender broker, providing a method to send email messages asynchronously.
/// </summary>
public interface IEmailSenderBroker
{
    /// <summary>
    /// Asynchronously sends an email message.
    /// </summary>
    /// <param name="emailMessage">The EmailMessage object representing the email to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete. Default is CancellationToken.None.</param>
    /// <returns>
    /// The result is true if the email was successfully sent; otherwise, false.
    /// </returns>
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}