using System.Linq.Expressions;
using AirBnB.Domain.Entities;
using AirBnB.Persistence.DataContexts;
using AirBnB.Persistence.Repositories.Interfaces;

namespace AirBnB.Persistence.Repositories;

public class UserRepository : EntityRepositoryBase<User, NotificationDbContext>, IUserRepository
{
    public UserRepository(NotificationDbContext dbContext) : base(dbContext)
    {
    }

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdsAsync(ids, asNoTracking, cancellationToken);
    }

    public ValueTask<User> CreateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> UpdateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.UpdateAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User> DeleteUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteAsync(user, saveChanges, cancellationToken);
    }

    public ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.DeleteByIdAsync(userId, saveChanges, cancellationToken);
    }
}