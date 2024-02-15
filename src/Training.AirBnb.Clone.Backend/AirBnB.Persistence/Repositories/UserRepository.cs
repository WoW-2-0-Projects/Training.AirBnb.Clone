﻿using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;
using EFCoreSecondLevelCacheInterceptor;

namespace AirBnB.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<User, AppDbContext>(
        dbContext,
        cacheBroker,
        new CacheEntryOptions()
    ), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate = default, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking)
            .Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(5));

    public new ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(userId, asNoTracking, cancellationToken);


    public new ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdsAsync(ids, asNoTracking, cancellationToken);


    public new ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(user, saveChanges, cancellationToken);


    public new ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.UpdateAsync(user, saveChanges, cancellationToken);


    public new ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteAsync(user, saveChanges, cancellationToken);


    public new ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(userId, saveChanges, cancellationToken);
}