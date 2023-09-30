using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;
using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Exceptions.NotificationExceptions.EmailTemplateExceptions;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;
public class EmailTemplateService : IEntityBaseService<EmailTemplate>
{
    private readonly IDataContext _dataContext;

    public EmailTemplateService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {

        if (!ValidationIsNull(emailTemplate))
            throw new EmailTemplateValidationToNull("This a member of these emailTemplate null");

        if (ValidationExits(emailTemplate))
            throw new EmailTemplateAlreadyExists("This emailTemplate already exists");

        await _dataContext.EmailTemplates.AddAsync(emailTemplate, cancellationToken);

        if (saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return emailTemplate;
    }

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> predicate)
    {
        return GetUndeletedEmailTemplate().Where(predicate.Compile()).AsQueryable();
    }

    public ValueTask<EmailTemplate> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var emailTemplate = GetUndeletedEmailTemplate().FirstOrDefault(emailTemplate => emailTemplate.Id == id);
        if (emailTemplate is null)
            throw new EmailTemplateNotFound("EmailTemplate not found");
        return new ValueTask<EmailTemplate>(emailTemplate);
    }

    public ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var emailTemplates = GetUndeletedEmailTemplate().Where(emailTemplate => ids.Contains(emailTemplate.Id));
        return new ValueTask<ICollection<EmailTemplate>>(emailTemplates.ToList());
    }

    public async ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (!ValidationIsNull(emailTemplate))
            throw new EmailTemplateValidationToNull("This a member of these emailTemplate null");

        var foundEmailTemplate = await GetByIdAsync(emailTemplate.Id);

        foundEmailTemplate.Subject = emailTemplate.Subject;
        foundEmailTemplate.Body = emailTemplate.Body;
        foundEmailTemplate.ModifiedDate = DateTimeOffset.UtcNow;

        if (saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;

    }

    public async ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var foundEmailTemplate = await GetByIdAsync(id);

        foundEmailTemplate.IsDeleted = true;
        foundEmailTemplate.DeletedDate = DateTimeOffset.UtcNow;

        if (saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {

        var foundEmailTemplate = await GetByIdAsync(emailTemplate.Id);

        foundEmailTemplate.IsDeleted = true;
        foundEmailTemplate.DeletedDate = DateTimeOffset.UtcNow;

        if (saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;
    }

    private bool ValidationIsNull(EmailTemplate emailTemplate)
    {
        if (string.IsNullOrEmpty(emailTemplate.Subject) || string.IsNullOrEmpty(emailTemplate.Body))
            return false;
        return true;
    }
    private bool ValidationExits(EmailTemplate emailTemplate)
    {
        var foundEmailTemplate = GetUndeletedEmailTemplate().FirstOrDefault(search => search.Equals(emailTemplate));
        if (foundEmailTemplate is null)
            return false;
        return true;
    }
    private IQueryable<EmailTemplate> GetUndeletedEmailTemplate() =>
        _dataContext.EmailTemplates.Where(emailTemplate => !emailTemplate.IsDeleted).AsQueryable();
}