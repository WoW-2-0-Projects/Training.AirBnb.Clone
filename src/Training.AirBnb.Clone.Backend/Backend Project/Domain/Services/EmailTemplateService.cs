using Backend_Project.Domain.Interfaces;
using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;
using Backend_Project.Domain.Exceptions;

namespace Backend_Project.Domain.Services;
public class EmailTemplateService : IEntityBaseService<EmailTemplate>
{
    private readonly IDataContext _dataContext;
    private readonly object _lock;

    public EmailTemplateService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {
        if(!ValidationToNull(emailTemplate))
            throw new EmailTemplateValidationToNull("This a member of these emailTemplate null");
        
        if(!ValidationToExists(emailTemplate))
            throw new EmailTemplateAlreadyExists("This emailTemplate already exists");
        
        await _dataContext.EmailTemplates.AddAsync(emailTemplate);
        
        if(saveChanges)
           await _dataContext.SaveChangesAsync();
        return emailTemplate;
    }

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> predicate)
    {
      return _dataContext.EmailTemplates.Where(predicate.Compile()).AsQueryable();
    }
    
    public ValueTask<EmailTemplate> GetByIdAsync(Guid id)
    {
        var emailTemplate = _dataContext.EmailTemplates.FirstOrDefault(emailTemplate => emailTemplate.Id == id);
        if (emailTemplate is null)
            throw new EmailTemplateNotFound("EmailTemplate not found");
        return new ValueTask<EmailTemplate>(emailTemplate);
    }
    
    public ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids)
    {
        var emailTemplates = _dataContext.EmailTemplates.Where(emailTemplate => ids.Contains(emailTemplate.Id));
        return new ValueTask<ICollection<EmailTemplate>>(emailTemplates.ToList());
    }
    
    public async ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {    
        if (!ValidationToNull(emailTemplate))
            throw new EmailTemplateValidationToNull("This a member of these emailTemplate null");

        var foundEmailTemplate = await GetByIdAsync(emailTemplate.Id);

        foundEmailTemplate.Subject = emailTemplate.Subject;
        foundEmailTemplate.Body = emailTemplate.Body;
        foundEmailTemplate.ModifiedDate = DateTimeOffset.UtcNow;

        if (saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;
        
    }
    
    public async ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true)
    {
        var foundEmailTemplate = await GetByIdAsync(id);
          
        foundEmailTemplate.IsDeleted = true; 
        foundEmailTemplate.DeletedDate = DateTimeOffset.UtcNow;
        
        if(saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {
        if (!ValidationToNull(emailTemplate))
            throw new EmailTemplateValidationToNull("This a member of these emailsTemplate null");
        
        var foundEmailTemplate = await GetByIdAsync(emailTemplate.Id);

        foundEmailTemplate.IsDeleted = true;
        foundEmailTemplate.DeletedDate = DateTimeOffset.UtcNow;
        
        if(saveChanges)
            await _dataContext.EmailTemplates.SaveChangesAsync();
        return foundEmailTemplate;
    }

    private bool ValidationToExists(EmailTemplate emailTemplate)
    {
        var foundEmailTemplate = _dataContext.EmailTemplates.FirstOrDefault(search => search.Subject.Equals(emailTemplate.Subject));
        
        if (foundEmailTemplate is not null)
            return false;
        return true;
    }

    private bool ValidationToNull(EmailTemplate emailTemplate)
    {
        var foundEmailTemplate = _dataContext.EmailTemplates.FirstOrDefault(search => search.Subject.Equals(emailTemplate.Subject));
        
        if (string.IsNullOrEmpty(emailTemplate.Subject) || string.IsNullOrEmpty(emailTemplate.Body))
            return false;
        return true;
    }
}
