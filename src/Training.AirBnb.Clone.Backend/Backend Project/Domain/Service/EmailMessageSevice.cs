using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;

namespace Backend_Project.Domain.Service;

public class EmailMessageSevice : IEmailMessageService<EmailMessage>
{
    public ValueTask<EmailMessage> ConvertToMessage(EmailMessage message, Dictionary<string, string> values, string sender, string receiver)
    {
        throw new NotImplementedException();
    }
}
