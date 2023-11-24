using System.Linq.Expressions;
using AirBnB.Domain.Entities;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface IUserRepository
{
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate, bool asNoTracking = false);

    ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<User> CreateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> UpdateUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User> DeleteUserAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<User?> DeleteByIdAsync(Guid userId, bool saveChanges = true, CancellationToken cancellationToken = default);
}