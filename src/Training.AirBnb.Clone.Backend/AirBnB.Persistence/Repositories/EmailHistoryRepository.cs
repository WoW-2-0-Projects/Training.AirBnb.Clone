using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories;

public class EmailHistoryRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<EmailHistory, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), IEmailHistoryRepository
{
    public new IQueryable<EmailHistory> Get(
        Expression<Func<EmailHistory, bool>>? predicate = null,
        bool asNoTracking = false) => base.Get(predicate, asNoTracking);

    public new ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(emailHistory, saveChanges, cancellationToken);
}
