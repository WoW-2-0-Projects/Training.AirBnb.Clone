using AirBnB.Application.Common.Notifications.Models;

namespace AirBnB.Application.Common.Notifications.Brokers;

/// <summary>
/// Interface for an SMS sender broker, providing a method to send SMS messages asynchronously.
/// </summary>
public interface ISmsSenderBroker
{
    /// <summary>
    /// Asynchronously sends an SMS message.
    /// </summary>
    /// <param name="smsMessage">The SmsMessage object representing the SMS to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete. Default is CancellationToken.None.</param>
    /// <returns>
    /// A ValueTask&lt;bool&gt; representing the asynchronous operation.
    /// The result is true if the SMS was successfully sent; otherwise, false.
    /// </returns>
    ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default);
}