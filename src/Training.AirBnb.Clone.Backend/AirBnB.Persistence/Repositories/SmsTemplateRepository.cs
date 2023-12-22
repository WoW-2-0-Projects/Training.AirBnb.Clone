using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories;

public class SmsTemplateRepository(NotificationsDbContext dbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<SmsTemplate, NotificationsDbContext>(dbContext, cacheBroker, new CacheEntryOptions()),
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