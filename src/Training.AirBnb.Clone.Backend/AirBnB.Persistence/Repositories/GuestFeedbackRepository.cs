using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class GuestFeedbackRepository(AppDbContext dbContext, ICacheBroker cacheBroker) 
    : EntityRepositoryBase<GuestFeedback, AppDbContext>(dbContext, cacheBroker, new CacheEntryOptions()), IGuestFeedbackRepository
{
    public new IQueryable<GuestFeedback> Get(
        Expression<Func<GuestFeedback, bool>>? predicate = default,
        bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public new async ValueTask<GuestFeedback?> GetByIdAsync(
        Guid id, 
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await base.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public new async ValueTask<GuestFeedback> CreateAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        return await base.CreateAsync(guestFeedback, saveChanges, cancellationToken);
    }
}