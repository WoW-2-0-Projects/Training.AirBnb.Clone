using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Enums;
using AirBnB.Domain.Extensions;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Notifications.Services;
/// <summary>
/// Implementation of the IEmailSenderService interface orchestrating the sending of email messages.
/// </summary>
public class EmailSenderService : IEmailSenderService
{
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;
    private readonly IValidator<EmailMessage> _emailMessageValidator;
    
    /// <summary>
    /// Initializes a new instance of the EmailSenderService class.
    /// </summary>
    /// <param name="emailSenderBroker">Collection of email sender brokers injected via dependency injection.</param>
    /// <param name="emailMessageValidator">Validator for email messages injected via dependency injection.</param>
    public EmailSenderService(
        IEnumerable<IEmailSenderBroker> emailSenderBroker,
        IValidator<EmailMessage> emailMessageValidator)
    {
        _emailSenderBrokers = emailSenderBroker;
        _emailMessageValidator = emailMessageValidator;
    }
    
    /// <summary>
    /// Sends an email asynchronously by orchestrating the process through available EmailSenderBrokers.
    /// </summary>
    /// <param name="emailMessage">The EmailMessage object representing the email to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete.</param>
    /// <returns>
    /// A ValueTask;bool; representing the asynchronous operation's success status.
    /// </returns>
    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
            var validationResult = _emailMessageValidator.Validate(emailMessage,
                options => options.IncludeRuleSets(NotificationEvent.OnSending.ToString()));

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            foreach (var smsSenderBroker in _emailSenderBrokers)
            {
                var sendNotifcationTask = () => smsSenderBroker.SendAsync(emailMessage, cancellationToken);
                var result = await sendNotifcationTask.GetValueAsync();

                emailMessage.IsSuccessful = result.IsSuccess;
                emailMessage.ErrorMessage = result.Exception.Message;

                return result.IsSuccess;
            }
            
            return false;
    }
}