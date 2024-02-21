using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Domain.Extension;
using AirBnB.Persistence.Extensions;
using FluentValidation;
using FluentValidation.Results;
using Twilio.TwiML.Voice;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

/// <summary>
/// Implementation of the ISmsSenderService interface orchestrating the sending of SMS messages.
/// </summary>
public class SmsSenderService(IEnumerable<ISmsSenderBroker> smsSenderBroker,
    IValidator<SmsMessage> smsMessageValidator) : ISmsSenderService
{
    /// <summary>
    /// Sends an SMS asynchronously by orchestrating the process through available SmsSenderBrokers.
    /// </summary>
    /// <param name="smsMessage">The SmsMessage object representing the SMS to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete.</param>
    /// <returns>A ValueTask&lt;bool&gt; representing the asynchronous operation's success status.</returns>
    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = smsMessageValidator.Validate(smsMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnRendering.ToString()));
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var accomplishedBroker = await smsSenderBroker.FirstOrDefaultAsync(async smsSenderBroker =>
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(smsMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();
            (smsMessage.IsSuccessful, smsMessage.ErrorMessage) = (result.IsSuccess, result.Exception?.Message);

            return result.IsSuccess;
        }, cancellationToken);
        return  false;
    }
}