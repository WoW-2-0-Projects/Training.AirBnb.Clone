using System.Linq.Expressions;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository for managing storage files, extending the base entity repository for <see cref="StorageFile"/>.
/// </summary>
public class StorageFileRepository(AppDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<StorageFile, AppDbContext>(
    dbContext,
    cacheBroker, 
    new CacheEntryOptions()), IStorageFileRepository
{
    public new IQueryable<StorageFile> Get(Expression<Func<StorageFile, bool>>? predicate = default,
        bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);
}