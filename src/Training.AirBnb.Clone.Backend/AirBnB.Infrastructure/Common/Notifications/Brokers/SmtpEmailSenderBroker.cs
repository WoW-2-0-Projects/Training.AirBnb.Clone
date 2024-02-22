using System.Net;
using System.Net.Mail;
using AirBnB.Application.Common.Notifications.Brokers;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Notifications.Brokers;

/// <summary>
/// Implementation of the IEmailSenderBroker interface using SMTP for sending email messages.
/// </summary>
public class SmtpEmailSenderBroker(IOptions<SmtpEmailSenderSettings> smtpEmailSenderSettings) : IEmailSenderBroker
{
    private readonly SmtpEmailSenderSettings _smtpEmailSenderSettings = smtpEmailSenderSettings.Value;
    
    /// <summary>
    /// Sends an email asynchronously using SMTP based on the provided EmailMessage and SMTP settings.
    /// </summary>
    /// <param name="emailMessage">The EmailMessage object representing the email to be sent.</param>
    /// <param name="cancellationToken">A CancellationToken to observe while waiting for the operation to complete.</param>
    /// <returns>A ValueTask&lt;bool&gt; representing the asynchronous operation's success status.</returns>
    public ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        emailMessage.SenderEmailAddress = _smtpEmailSenderSettings.CredentialAddress;

        var mail = new MailMessage(emailMessage.SenderEmailAddress, emailMessage.ReceiverEmailAddress);
        mail.Subject = emailMessage.Subject;
        mail.Body = emailMessage.Body;
        mail.IsBodyHtml = true;
        
        var smtpClient = new SmtpClient(_smtpEmailSenderSettings.Host, _smtpEmailSenderSettings.Port);
        smtpClient.Credentials =
            new NetworkCredential(_smtpEmailSenderSettings.CredentialAddress, _smtpEmailSenderSettings.Password);
        smtpClient.EnableSsl = true;
        
        smtpClient.Send(mail);

        return new ValueTask<bool>(true);
    }
}