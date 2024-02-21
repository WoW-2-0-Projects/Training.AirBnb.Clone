using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class SmsTemplateRepository(AppDbContext dbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<SmsTemplate, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), ISmsTemplateRepository
{
    public new IQueryable<SmsTemplate> Get(
        Expression<Func<SmsTemplate, bool>>? predicate = default,
        bool asNoTracking = false) => 
        base.Get(predicate, asNoTracking);
    
    public new async ValueTask<SmsTemplate> CreateAsync(
        SmsTemplate smsTemplate,
        bool saveChanges = true,
        CancellationToken cancellationToken = default) =>
        await base.CreateAsync(smsTemplate, saveChanges, cancellationToken);
}