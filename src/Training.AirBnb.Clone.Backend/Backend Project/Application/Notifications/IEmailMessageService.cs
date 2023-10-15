using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Notifications;

public interface IEmailMessageService
{
    ValueTask<EmailMessage> ConvertToMessage(EmailTemplate entity, Dictionary<string, string> values, string sender, string receiver);
}