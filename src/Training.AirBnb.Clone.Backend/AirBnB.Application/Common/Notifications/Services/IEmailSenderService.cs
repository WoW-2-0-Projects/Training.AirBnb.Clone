using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Services;
/// <summary>
/// Interface for an email sender service, providing methods to send email messages asynchronously.
/// </summary>
public interface IEmailSenderService
{
    /// <summary>
    /// Asynchronously sends a message to the specified email address using the specified parameters.
    /// </summary>
    /// <param name="emailMessage">The EmailMessage object representing the email to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete. Default is CancellationToken.None.</param>
    /// <returns>
    /// The result is true if the email was successfully sent; otherwise, false.
    /// </returns>
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}