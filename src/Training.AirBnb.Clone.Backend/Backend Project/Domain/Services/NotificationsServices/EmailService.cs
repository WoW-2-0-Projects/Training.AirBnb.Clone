using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Enums;
using Backend_Project.Domain.Exceptions.NotificationExceptions.EmailExceptions;
using Backend_Project.Domain.Interfaces;
using Backend_Project.Persistance.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Domain.Services.NotificationsServices;

public class EmailService : IEntityBaseService<Email>
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    

    public EmailService(IDataContext dataContext, IValidationService validationService)
    {
        _appDataContext = dataContext;
        _validationService = validationService;
    }
    public async ValueTask<Email> CreateAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidationIsNull(email))
            throw new EmailValidationIsNull("This a member of these emailTemplate null");

        await _appDataContext.Emails.AddAsync(email,cancellationToken);
        
        if(saveChanges) await _appDataContext.Emails.SaveChangesAsync(cancellationToken);
        
        return email;
    }

    public IQueryable<Email> Get(Expression<Func<Email, bool>> predicate)
    {
        return _appDataContext.Emails.Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<ICollection<Email>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var emails = _appDataContext.Emails.Where(email => ids.Contains(email.Id));
        return new ValueTask<ICollection<Email>>(emails.ToList());
    }

    public ValueTask<Email> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var email = _appDataContext.Emails.FirstOrDefault(email => email.Id == id);
        
        if (email is null)
            throw new EmailNotFound("Email not found");
        
        return new ValueTask<Email>(email);
    }

    //This method is deprecated in Email Preparation
    public ValueTask<Email> UpdateAsync(Email entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    //This method is deprecated in Email Preparation
    public ValueTask<Email> DeleteAsync(Email entity, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    //This method is deprecated in Email Preparation
    public ValueTask<Email> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    private bool ValidationIsNull(Email email)
    {
        if (string.IsNullOrEmpty(email.Subject)
            || string.IsNullOrEmpty(email.Body)
            || !_validationService.IsValidEmailAddress(email.ReceiverEmailAddres)
            || !_validationService.IsValidEmailAddress(email.SenderEmailAddress))
            return false;
        return true;
    }    
}