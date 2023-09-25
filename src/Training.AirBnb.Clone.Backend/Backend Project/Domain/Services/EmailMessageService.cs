using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;

namespace Backend_Project.Domain.Services;

public class EmailMessageService : IEmailMessage<EmailMessage>
{
    public ValueTask<EmailMessage> ConvertToMessage(EmailMessage entity, Dictionary<string, string> values, string sender, string receiver)
    {
        throw new NotImplementedException();
    }
}
