using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Enums;
using AirBnB.Domain.Extensions;
using AirBnB.Persistence.Extensions;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Notifications.Services;
/// <summary>
/// Implementation of the IEmailSenderService interface orchestrating the sending of email messages.
/// </summary>
public class EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBroker,
    IValidator<EmailMessage> emailMessageValidator) : IEmailSenderService
{
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
        var validationResult = await emailMessageValidator.ValidateAsync(emailMessage,
            options => options.IncludeRuleSets(NotificationEvent.OnSending.ToString()), cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await emailSenderBroker.FirstOrDefaultAsync( async emailSenderBrokers =>
        {
            var sendNotificationTask = () => emailSenderBrokers.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();
            (emailMessage.IsSuccessful, emailMessage.ErrorMessage) = (result.IsSuccess, result.Exception?.Message);

            return result.IsSuccess;
        }, cancellationToken);
        
        return false;
    }
}