using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Service;

public class EmailMessageSevice : IEmailMessageService<EmailMessage>, IEntityBaseService<EmailMessage>
{
    public ValueTask<EmailMessage> ConvertToMessage(EmailMessage message, Dictionary<string, string> values, string sender, string receiver)
    {
        throw new NotImplementedException();
    }

    ValueTask<EmailMessage> IEntityBaseService<EmailMessage>.CreateAsync(EmailMessage entity, bool saveChanges)
    {
        throw new NotImplementedException();
    }

    IQueryable<EmailMessage> IEntityBaseService<EmailMessage>.Get(Expression<Func<EmailMessage, bool>> predicate)
    {
        throw new NotImplementedException();
    }
    
    ValueTask<EmailMessage> IEntityBaseService<EmailMessage>.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    ValueTask<ICollection<EmailMessage>> IEntityBaseService<EmailMessage>.GetAsync(IEnumerable<Guid> ids)
    {
        throw new NotImplementedException();
    }
    
    ValueTask<EmailMessage> IEntityBaseService<EmailMessage>.DeleteAsync(Guid id, bool saveChanges)
    {
        throw new NotImplementedException();
    }

    ValueTask<EmailMessage> IEntityBaseService<EmailMessage>.DeleteAsync(EmailMessage entity, bool saveChanges)
    {
        throw new NotImplementedException();
    }

    ValueTask<EmailMessage> IEntityBaseService<EmailMessage>.UpdateAsync(EmailMessage entity, bool saveChanges)
    {
        throw new NotImplementedException();
    }

    private bool ValidationToNull(EmailMessage emailMessage)
    {
        if(emailMessage is null)
            return false;
        return true;
    }
    
    private void ValidationExists(EmailMessage emailMessage)
    {

    }
}
