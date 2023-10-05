using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;

public interface IEmailMessageService
{
    ValueTask<EmailMessage> ConvertToMessage(EmailTemplate entity, Dictionary<string, string> values, string sender, string receiver);
}