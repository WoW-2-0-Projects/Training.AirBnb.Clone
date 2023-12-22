using System.Linq.Expressions;
using AirBnB.Domain.Common;
using AirBnB.Domain.Common.Caching;
using AirBnB.Domain.Common.Query;
using AirBnB.Persistence.Caching.Brokers;
using AirBnB.Persistence.Caching.Models;
using AirBnB.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.Repositories;

/// <summary>
/// Represents a base repository for entities with common CRUD operations.
/// </summary>
/// <param name="dbContext"></param>
/// <param name="cacheBroker"></param>
/// <param name="cacheEntryOptions"></param>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class EntityRepositoryBase<TEntity, TContext>(
    TContext dbContext,
    ICacheBroker cacheBroker,
    CacheEntryOptions? cacheEntryOptions = default)
    where TEntity : class, IEntity where TContext : DbContext
{
    protected TContext DbContext => dbContext;

    /// <summary>
    /// Retrieves entities from the repository based on optional filtering conditions and tracking preferences.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="asNoTracking"></param>
    /// <returns></returns>
    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;
    }

    /// <summary>
    ///  Asynchronously retrieves entities from the repository based on a query specification and caching options.
    /// </summary>
    /// <param name="querySpecification"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<IList<TEntity>> GetAsync(QuerySpecification<TEntity> querySpecification,
        CancellationToken cancellationToken = default)
    {
        var foundEntities = new List<TEntity>();

        var cacheKey = querySpecification.CacheKey;

        if (cacheEntryOptions is null || !await cacheBroker.TryGetAsync<List<TEntity>>(cacheKey, out var cachedEntity))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();

            if (querySpecification.AsNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            initialQuery = initialQuery.ApplySpecification(querySpecification);

            foundEntities = await initialQuery.ToListAsync(cancellationToken);

            if (cacheEntryOptions is not null)
                await cacheBroker.SetAsync(cacheKey, foundEntities, cacheEntryOptions);
        }
        else
            foundEntities = cachedEntity;

        return foundEntities;
    }

    protected async ValueTask<IList<TEntity>> GetAsync(QuerySpecification querySpecification,
        CancellationToken cancellationToken = default)
    {
        var foundEntities = new List<TEntity>();

        var cacheKey = querySpecification.CacheKey;

        if (cacheEntryOptions is null || !await cacheBroker.TryGetAsync<List<TEntity>>(cacheKey, out var cachedEntity))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();

            if (querySpecification.AsNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            initialQuery = initialQuery.ApplySpecification(querySpecification);

            foundEntities = await initialQuery.ToListAsync(cancellationToken);

            if (cacheEntryOptions is not null)
                await cacheBroker.SetAsync(cacheKey, foundEntities, cacheEntryOptions);
        }
        else
            foundEntities = cachedEntity;

        return foundEntities;
    }
    
    /// <summary>
    /// Asynchronously retrieves an entity from the repository by its ID, optionally applying caching.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity?> GetByIdAsync(
        Guid id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var foundEntity = default(TEntity?);

        if (cacheEntryOptions is null || !await cacheBroker.TryGetAsync<TEntity>(id.ToString(), out var cachedEntity))
        {
            var initialQuery = DbContext.Set<TEntity>().AsQueryable();

            if (asNoTracking)
                initialQuery = initialQuery.AsNoTracking();

            foundEntity = await initialQuery.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

            if (cacheEntryOptions is not null && foundEntity is not null)
                await cacheBroker.SetAsync(foundEntity.Id.ToString(), foundEntity, cacheEntryOptions);
        }
        else
            foundEntity = cachedEntity;

        return foundEntity;
    }

    /// <summary>
    /// Asynchronously retrieves entities from the repository by a collection of IDs.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<IList<TEntity>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => ids.Contains(entity.Id));

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return await initialQuery.ToListAsync(cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Asynchronously creates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> CreateAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        if (cacheEntryOptions is not null)
            await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously updates a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity> UpdateAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Update(entity);

        if (cacheEntryOptions is not null)
            await cacheBroker.SetAsync(entity.Id.ToString(), entity, cacheEntryOptions);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously deletes a new entity in the repository.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async ValueTask<TEntity?> DeleteAsync(
        TEntity entity,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);

        if (cacheEntryOptions is not null)
            await cacheBroker.DeleteAsync(entity.Id.ToString());

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Asynchronously deletes an existing entity from the repository by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="saveChanges"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected async ValueTask<TEntity?> DeleteByIdAsync(
        Guid id,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
                     throw new InvalidOperationException();

        DbContext.Set<TEntity>().Remove(entity);

        if (cacheEntryOptions is not null)
            await cacheBroker.DeleteAsync(entity.Id.ToString());

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    private string AddTypePrefix(CacheModel model)
    {
        return $"{typeof(TEntity).Name}_{model.CacheKey}";
    }
}