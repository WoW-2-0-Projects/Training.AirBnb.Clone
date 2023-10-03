using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using System.Net;
using System.Net.Mail;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailSenderService : IEmailSenderService
{
    public async ValueTask<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        var result = false;
        try
        {
            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.Credentials = new NetworkCredential("sultonbek.rakhimov.recovery@gmail.com", "szabguksrhwsbtie");
                smtp.EnableSsl = true;

                var mail = new MailMessage(emailMessage.SenderAddress, emailMessage.ReceiverAddress);
                mail.Subject = emailMessage.Subject;
                mail.Body = emailMessage.Body;

                await smtp.SendMailAsync(mail);
            }
            emailMessage.IsSent = true;
            emailMessage.SendDate = DateTimeOffset.UtcNow;
            result = true;
        }
        catch(EntityException<Exception> exception)
        {
            emailMessage.IsSent = false;
            emailMessage.SendDate = DateTimeOffset.UtcNow;
            result = false;
        }

        return result;     
    }
}
