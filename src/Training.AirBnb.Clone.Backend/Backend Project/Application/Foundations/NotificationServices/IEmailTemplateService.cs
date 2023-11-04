using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.NotificationServices;

public interface IEmailTemplateService
{
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>> predicate);

    ValueTask<ICollection<EmailTemplate>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> UpdateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> DeleteAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}