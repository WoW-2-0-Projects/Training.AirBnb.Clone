﻿using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Identity;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class UserRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<User, IdentityDbContext>(
        dbContext,
        cacheBroker,
        new CacheEntryOptions()
    ), IUserRepository
{
    public new IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public new ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification,
        CancellationToken cancellationToken = default)
        => base.GetAsync(querySpecification, cancellationToken);

    public new ValueTask<IList<User>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default)
        =>
            base.GetAsync(querySpecification, cancellationToken);


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