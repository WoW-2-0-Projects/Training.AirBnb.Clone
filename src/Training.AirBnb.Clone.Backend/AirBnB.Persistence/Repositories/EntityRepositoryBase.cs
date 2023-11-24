using System.Linq.Expressions;
using AirBnB.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Persistence.Repositories;

public abstract class EntityRepositoryBase<TEntity, TContext> where TEntity : class, IEntity where TContext : DbContext
{
    private TContext DbContext => _dbContext;
    private readonly TContext _dbContext;

    protected EntityRepositoryBase(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected IQueryable<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = default, bool asNoTracking = false)
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (predicate is not null)
            initialQuery = initialQuery.Where(predicate);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();

        return initialQuery;

    }

    protected async ValueTask<TEntity?> GetByIdAsync(
                    Guid id,
                    bool asNoTracking = false,
                    CancellationToken cancellationToken = default
    )
    {
        var initialQuery = DbContext.Set<TEntity>().Where(entity => true);

        if (asNoTracking)
            initialQuery = initialQuery.AsNoTracking();
        
        return await initialQuery.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

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

    protected async ValueTask<TEntity> CreateAsync(
                    TEntity entity,
                    bool saveChanges = true,
                    CancellationToken cancellationToken = default
    )
    {
        entity.Id = Guid.Empty;

        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    protected async ValueTask<TEntity> UpdateAsync(
                    TEntity entity,
                    bool saveChanges = true,
                    CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Update(entity);

        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    protected async ValueTask<TEntity> DeleteAsync(
                    TEntity entity,
                    bool saveChanges = true,
                    CancellationToken cancellationToken = default
    )
    {
        DbContext.Set<TEntity>().Remove(entity);
        
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    protected async ValueTask<TEntity?> DeleteByIdAsync(
                    Guid id,
                    bool saveChanges = true,
                    CancellationToken cancellationToken = default
    )
    {
        var entity = await DbContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken) ??
                     throw new InvalidOperationException();
        
        DbContext.Set<TEntity>().Remove(entity);
        
        if (saveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
}
