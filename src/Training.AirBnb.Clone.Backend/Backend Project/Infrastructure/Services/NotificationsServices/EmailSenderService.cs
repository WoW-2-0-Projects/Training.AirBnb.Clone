using Backend_Project.Application.Notifications.Services;
using Backend_Project.Application.Notifications.Settings;
using Backend_Project.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSenderSettings _senderSettings;

    public EmailSenderService(IOptions<EmailSenderSettings> senderSettings) 
        => _senderSettings = senderSettings.Value;

    public async ValueTask<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        bool result;
        try
        {
            using (var smtp = new SmtpClient(_senderSettings.SmtpClient, _senderSettings.SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(_senderSettings.CredentialEmailAddress, _senderSettings.CredentialPassword);
                smtp.EnableSsl = true;

                var mail = new MailMessage(emailMessage.SenderAddress, emailMessage.ReceiverAddress)
                {
                    Subject = emailMessage.Subject,
                    Body = emailMessage.Body
                };

                await smtp.SendMailAsync(mail);
            }
            emailMessage.IsSent = true;
            emailMessage.SendDate = DateTimeOffset.UtcNow;
            result = true;
        }
        catch(Exception)
        {
            emailMessage.IsSent = false;
            emailMessage.SendDate = DateTimeOffset.UtcNow;
            result = false;
        }

        return result;     
    }
}