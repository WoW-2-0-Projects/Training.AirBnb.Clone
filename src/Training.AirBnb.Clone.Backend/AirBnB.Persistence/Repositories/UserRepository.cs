using System.Linq.Expressions;
using AirBnB.Domain.Common.Caching;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.Caching;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class UserRepository(IdentityDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<User, IdentityDbContext>(dbContext, cacheBroker), IUserRepository
{
    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false)
        => base.Get(predicate, asNoTracking);

    public ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
        => base.GetAsync(querySpecification, asNoTracking, cancellationToken);


    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdAsync(userId, asNoTracking, cancellationToken);


    public ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
        => base.GetByIdsAsync(ids, asNoTracking, cancellationToken);


    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.CreateAsync(user, saveChanges, cancellationToken);


    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.UpdateAsync(user, saveChanges, cancellationToken);


    public ValueTask<User?> DeleteAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteAsync(user, saveChanges, cancellationToken);


    public ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default)
        => base.DeleteByIdAsync(userId, saveChanges, cancellationToken);
}

