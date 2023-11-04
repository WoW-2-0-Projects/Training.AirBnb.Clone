using Backend_Project.Application.Foundations.NotificationServices;
using Backend_Project.Application.Validation;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Persistence.DataContexts;
using System.Linq.Expressions;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailService : IEmailService
{
    private readonly IDataContext _appDataContext;
    private readonly IValidationService _validationService;
    public EmailService(IDataContext appDataContext, IValidationService validationService)
    {
        _appDataContext = appDataContext;
        _validationService = validationService;
    }
    public async ValueTask<Email> CreateAsync(Email email, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidationToNull(email))
            throw new EntityValidationException<Email>("This a member of these emailTemplate null");

        await _appDataContext.Emails.AddAsync(email, cancellationToken);

        if (saveChanges) await _appDataContext.SaveChangesAsync();

        return email;
    }

    public ValueTask<ICollection<Email>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var emails = _appDataContext.Emails.Where(email => ids.Contains(email.Id));
        return new ValueTask<ICollection<Email>>(emails.ToList());
    }

    public ValueTask<Email> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var email = _appDataContext.Emails.FirstOrDefault(email => email.Id == id);

        return email is null ? throw new EntityNotFoundException<Email>("Email not found") 
            : new ValueTask<Email>(email);
    }

    public IQueryable<Email> Get(Expression<Func<Email, bool>> predicate)
    {
        return _appDataContext.Emails.Where(predicate.Compile()).AsQueryable();
    }

    //Validation method
    private bool ValidationToNull(Email email)
    {
        if (string.IsNullOrWhiteSpace(email.Subject)
            || string.IsNullOrWhiteSpace(email.Body)
            || !_validationService.IsValidEmailAddress(email.ReceiverEmailAddress)
            || !_validationService.IsValidEmailAddress(email.SenderEmailAddress))
            return false;
        
        return true;
    }
}