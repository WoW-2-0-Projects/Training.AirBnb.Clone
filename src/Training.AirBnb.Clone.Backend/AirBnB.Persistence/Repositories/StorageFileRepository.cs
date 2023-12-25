﻿using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.StorageFiles;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Repository for managing storage files, extending the base entity repository for <see cref="StorageFile"/>.
/// </summary>
public class StorageFileRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker) : EntityRepositoryBase<StorageFile, IdentityDbContext>(
    dbContext,
    cacheBroker, 
    new CacheEntryOptions()), IStorageFileRepository
{
    public new ValueTask<IList<StorageFile>> GetAsync(QuerySpecification<StorageFile> querySpecification,
        CancellationToken cancellationToken = default)
        => base.GetAsync(querySpecification, cancellationToken);

    public new ValueTask<IList<StorageFile>> GetAsync(QuerySpecification querySpecification, CancellationToken cancellationToken = default)
        => base.GetAsync(querySpecification, cancellationToken);
}