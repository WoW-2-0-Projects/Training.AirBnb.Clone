using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Notifications.Services;

public interface IEmailMessageService
{
    ValueTask<EmailMessage> ConvertToMessage(EmailTemplate emailTemplate, IEnumerable<KeyValuePair<string, string>> values, Guid senderUserId, Guid receiverUserId);
}