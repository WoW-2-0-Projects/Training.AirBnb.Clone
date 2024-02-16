using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories;

public class SmsHistoryRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<SmsHistory, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), ISmsHistoryRepository
{
    public new IQueryable<SmsHistory> Get(Expression<Func<SmsHistory, bool>>? predicate = null, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<SmsHistory> CreateAsync(SmsHistory smsHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(smsHistory, saveChanges, cancellationToken);
}
