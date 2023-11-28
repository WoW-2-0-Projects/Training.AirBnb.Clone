using AirBnB.Domain.Entities;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class EmailTemplateRepository(NotificationsDbContext dbContext) : 
    EntityRepositoryBase<EmailTemplate, NotificationsDbContext>(dbContext),
    IEmailTemplateRepository
{
    public new async ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        bool saveChanges = true, 
        CancellationToken cancellationToken = default) => 
            await base.CreateAsync(emailTemplate, saveChanges, cancellationToken);

    public new IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? 
        predicate = default, bool asNoTracking = false)
            => base.Get(predicate, asNoTracking);
}