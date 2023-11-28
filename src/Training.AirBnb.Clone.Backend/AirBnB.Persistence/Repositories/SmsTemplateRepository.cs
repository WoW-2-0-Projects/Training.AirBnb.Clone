using AirBnB.Domain.Entities;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class SmsTemplateRepository(NotificationsDbContext dbContext) :
    EntityRepositoryBase<SmsTemplate, NotificationsDbContext>(dbContext),
        ISmsTemplateRepository
{
    public new async ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges, 
        CancellationToken cancellationToken) =>
            await base.CreateAsync(smsTemplate, saveChanges, cancellationToken);

    public new IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? 
        predicate, bool asNoTracking) =>
            base.Get(predicate, asNoTracking);
}