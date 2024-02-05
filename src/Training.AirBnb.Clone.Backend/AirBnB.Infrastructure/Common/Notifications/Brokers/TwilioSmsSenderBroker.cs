using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AirBnB.Infrastructure.Common.Notifications.Brokers;

/// <summary>
/// Implementation of ISmsSenderBroker interface using Twilio for sending sms messages to the specified number
/// </summary>
public class TwilioSmsSenderBroker(IOptions<TwilioSmsSenderSettings> twilioSmsSenderSettings) : ISmsSenderBroker
{
    private readonly TwilioSmsSenderSettings _twilioSmsSenderSettings = twilioSmsSenderSettings.Value;
    
    /// <summary>
    /// Sends an SMS asynchronously using Twilio based on the provided SmsMessage and Twilio settings.
    /// </summary>
    /// <param name="smsMessage">The SmsMessage object representing the SMS to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete.</param>
    /// <returns>A ValueTask&lt;bool&gt; representing the asynchronous operation's success status.</returns>
    public ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        TwilioClient.Init(_twilioSmsSenderSettings.AccountsId, _twilioSmsSenderSettings.AuthToken);

        var messageToken = MessageResource.Create(
            body: smsMessage.Message,
            from: new Twilio.Types.PhoneNumber(_twilioSmsSenderSettings.SenderPhoneNumber),
            to: new Twilio.Types.PhoneNumber(smsMessage.ReceiverPhoneNumber)
            );

        return new ValueTask<bool>(true);
    }
}