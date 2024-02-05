using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Services;
/// <summary>
/// Interface for sms sender service, providing methods to send messages asynchronously
/// </summary>
public interface ISmsSenderService
{
    /// <summary>
    /// Asynchronously sends sms message.
    /// </summary>
    /// <param name="smsMessage">The SmsMessage object representing the Sms to be sent</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting operation to complete. Default is CancellationToken.None.</param>
    /// <returns>
    /// The result is true if the sms was successfully sent; otherwise, false.
    /// </returns>
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}