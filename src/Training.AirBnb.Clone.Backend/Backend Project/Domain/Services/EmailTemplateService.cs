using Backend_Project.Domain.Interfaces;
using Backend_Project.Domain.Entities;
using System.Linq.Expressions;
using Backend_Project.Persistance.DataContexts;
using Backend_Project.Domain.Exceptions;

namespace Backend_Project.Domain.Services;
public class EmailTemplateService : IEntityBaseService<EmailTemplate>
{
    private readonly IDataContext _dataContext;

    public EmailTemplateService(IDataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {
        await _dataContext.EmailTemplates.AddAsync(emailTemplate);
        if(saveChanges)
           await _dataContext.SaveChangesAsync();
        return emailTemplate;
    }

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> predicate)
    {
      return _dataContext.EmailTemplates.Where(predicate.Compile()).AsQueryable();
    }
    
    public ValueTask<EmailTemplate?> GetByIdAsync(Guid id)
    {
        var emailTemplate = _dataContext.EmailTemplates.FirstOrDefault(emailTemplate => emailTemplate.Id == id);
        return new ValueTask<EmailTemplate?>(emailTemplate);
    }
    
    public ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids)
    {
        var emailTemplates = _dataContext.EmailTemplates.Where(emailTemplate => ids.Contains(emailTemplate.Id));
        return new ValueTask<ICollection<EmailTemplate>>(emailTemplates.ToList());
    }
    
    public async ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {
        var foundEmailTemplate = _dataContext.EmailTemplates.FirstOrDefault(searched => searched.Id == emailTemplate.Id);
        if (foundEmailTemplate is null)
            throw new EmailTemplateException("EmailTemplate not found");
        foundEmailTemplate.Subject = emailTemplate.Subject;
        foundEmailTemplate.Body = emailTemplate.Body;
        
        await _dataContext.SaveChangesAsync();
        return foundEmailTemplate;
    }
    
    public async ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true)
    {
      var foundemailTemplate = await GetByIdAsync(id);
        if (foundemailTemplate is null)
            throw new EmailTemplateException("You searched emailTemplate not found");
        foundemailTemplate.IsDeleted = true; 
        await _dataContext.SaveChangesAsync();
        return foundemailTemplate;
    }

    public async ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true)
    {
        var foundemailTemplate = await GetByIdAsync(emailTemplate.Id);
        if (foundemailTemplate is null)
            throw new EmailTemplateException("You searched emailTemplate not found");

        await _dataContext.SaveChangesAsync();
        return foundemailTemplate;
    }

}
