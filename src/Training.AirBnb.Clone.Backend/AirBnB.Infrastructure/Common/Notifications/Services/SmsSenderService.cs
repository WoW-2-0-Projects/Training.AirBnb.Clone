using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Enums;
using AirBnB.Domain.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace AirBnB.Infrastructure.Common.Notifications.Services;

/// <summary>
/// Implementation of the ISmsSenderService interface orchestrating the sending of SMS messages.
/// </summary>
public class SmsSenderService : ISmsSenderService
{
    private readonly IEnumerable<ISmsSenderBroker> _smsSenderBrokers;
    private readonly IValidator<SmsMessage> _smsMessageValidators;

    /// <summary>
    /// Initializes a new instance of the SmsSenderService class.
    /// </summary>
    /// <param name="smsSenderBrokers">Collection of SMS sender brokers injected via dependency injection.</param>
    /// <param name="smsMessageValidator">Validator for SMS messages injected via dependency injection.</param>
    public SmsSenderService(
        IEnumerable<ISmsSenderBroker> smsSenderBrokers,
        IValidator<SmsMessage> smsMessageValidator)
    {
        _smsSenderBrokers = smsSenderBrokers;
        _smsMessageValidators = smsMessageValidator;
    }
    
    /// <summary>
    /// Sends an SMS asynchronously by orchestrating the process through available SmsSenderBrokers.
    /// </summary>
    /// <param name="smsMessage">The SmsMessage object representing the SMS to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete.</param>
    /// <returns>A ValueTask&lt;bool&gt; representing the asynchronous operation's success status.</returns>
    public async ValueTask<bool> SendAsync(SmsMessage smsMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _smsMessageValidators.Validate(smsMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnRendering.ToString()));

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        foreach (var smsSenderBroker in _smsSenderBrokers)
        {
            var sendNotificationTask = () => smsSenderBroker.SendAsync(smsMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            smsMessage.IsSuccessful = result.IsSuccess;
            smsMessage.ErrorMessage = result.Exception.Message;
            return result.IsSuccess;
        }

        return false;
    }
}