using Backend_Project.Domain.Interfaces;
using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;
using Backend_Project.Domain.Exceptions.EmailMessageExceptions;

namespace Backend_Project.Domain.Services;

public class EmailMessageSevice : IEntityBaseService<EmailMessage>
{
    private readonly IDataContext _dataContext;

    public EmailMessageSevice(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async ValueTask<EmailMessage> CreateAsync(EmailMessage emailMessage, bool saveChanges)
    {
        if (ValidationToNull(emailMessage))
            throw new EmailMessageValidationToNull("This a member of these emailMessage null");
        
        if (ValidationExists(emailMessage))
            throw new EmailMessageAlreadyExists("This emailTemplate already exists");
        
        await _dataContext.EmailMessages.AddAsync(emailMessage);

        if (saveChanges)
            await _dataContext.SaveChangesAsync();
        return emailMessage;
    }

    public IQueryable<EmailMessage> Get(Expression<Func<EmailMessage, bool>> predicate)
    {
        return UndeletedEmailMessage().Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<EmailMessage> GetByIdAsync(Guid id)
    {
        var emailMessage = UndeletedEmailMessage().FirstOrDefault(emailMessage => emailMessage.Id == id);
        if (emailMessage is null)
            throw new EmailMessageNotFound("EmailTemplate not found");
        return new ValueTask<EmailMessage>(emailMessage);
    }

    public ValueTask<ICollection<EmailMessage>> GetAsync(IEnumerable<Guid> ids)
    {
       var emailMessages = UndeletedEmailMessage().Where(emailMessage => ids.Contains(emailMessage.Id));
        return new ValueTask<ICollection<EmailMessage>>(emailMessages.ToList());
    }

    public async ValueTask<EmailMessage> UpdateAsync(EmailMessage emailMessage, bool saveChanges)
    {
        if (ValidationExists(emailMessage))
            throw new EmailMessageValidationToNull("This a member of these emailTemplate null");
        var foundEmailMessage = await GetByIdAsync(emailMessage.Id);

        foundEmailMessage.Subject = emailMessage.Subject;
        foundEmailMessage.Body = emailMessage.Body;
        foundEmailMessage.SerdorAddress = emailMessage.SerdorAddress;
        foundEmailMessage.ReceiverAddress = emailMessage.ReceiverAddress;
        foundEmailMessage.ModifiedDate = DateTimeOffset.UtcNow;


        if(saveChanges)
            await _dataContext.SaveChangesAsync();
        return foundEmailMessage;
    }
    
    public async ValueTask<EmailMessage> DeleteAsync(Guid id, bool saveChanges)
    {
        var foundEmailMessage = await GetByIdAsync(id);

        foundEmailMessage.IsDeleted = true;
        foundEmailMessage.DeletedDate = DateTimeOffset.UtcNow;

        if(saveChanges)
            await _dataContext.SaveChangesAsync();
        return foundEmailMessage;
    }

    public async ValueTask<EmailMessage> DeleteAsync(EmailMessage emailMessage, bool saveChanges)
    {
        var foundEmailMessage = await GetByIdAsync(emailMessage.Id);
        foundEmailMessage.IsDeleted = true;
        foundEmailMessage.DeletedDate= DateTimeOffset.UtcNow;

        if(saveChanges)
            await _dataContext.SaveChangesAsync();
        return foundEmailMessage;
    }

    private bool ValidationToNull(EmailMessage emailMessage)
    {
        if (string.IsNullOrEmpty(emailMessage.Subject) 
            || string.IsNullOrEmpty(emailMessage.Body)
            || string.IsNullOrEmpty(emailMessage.ReceiverAddress)
            || string.IsNullOrEmpty(emailMessage.SerdorAddress)) 
            return false;
        return true;
    }

    private bool ValidationExists(EmailMessage emailMessage)
    {
       var foundEmailMessage = UndeletedEmailMessage().FirstOrDefault(search => search.Equals(emailMessage));
        if (foundEmailMessage is null)
            return false;
        return true;

    }

    private IQueryable<EmailMessage> UndeletedEmailMessage()=> 
        _dataContext.EmailMessages.Where(emailMEssage => !emailMEssage.IsDeleted).AsQueryable();
    
    public ValueTask<EmailMessage> ConvertToMessage(EmailTemplate emailTemplate, Dictionary<string, string> values, string sender, string receiver)
    {
        var body = emailTemplate.Body;
        foreach(var value in values)
        {
            body = body.Replace(value.Key, value.Value);
        }
        var emailMessage = new EmailMessage(emailTemplate.Subject, emailTemplate.Body, sender, receiver);
        return ValueTask.FromResult(emailMessage);
    }
}
