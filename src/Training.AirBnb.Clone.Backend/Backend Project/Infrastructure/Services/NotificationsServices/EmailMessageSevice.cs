using Backend_Project.Domain.Entities;
using Backend_Project.Application.Interfaces;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailMessageSevice : IEmailMessageService
{
    public ValueTask<EmailMessage> ConvertToMessage(EmailTemplate emailTemplate, Dictionary<string, string> values, string sender, string receiver)
    {
       var body = emailTemplate.Body;

        foreach (var item in values)
        {
            body = body.Replace(item.Key, item.Value);
        }

        var EmailMessage = new EmailMessage(emailTemplate.Subject, body, sender, receiver);

        return ValueTask.FromResult(EmailMessage);
    }
}