using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Notifications;

public interface IEmailMessageService
{
    ValueTask<EmailMessage> ConvertToMessage(EmailTemplate entity, Dictionary<string, string> values, Guid senderUserId, Guid receiverUserId);
}