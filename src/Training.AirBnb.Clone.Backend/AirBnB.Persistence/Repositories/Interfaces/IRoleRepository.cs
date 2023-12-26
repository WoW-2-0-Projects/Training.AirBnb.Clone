using AirBnB.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;
/// <summary>
/// This interface defines the contract for interacting with Role entities in the repository.
/// </summary>
public interface IRoleRepository
{
    // Retrieves IQueryable collection of Role entities based on the specified predicate.
    // - 'predicate': Optional filter condition expressed as a lambda expression.
    // - 'asNoTracking': Indicates whether to track changes (default is false).
    IQueryable<Role> Get(Expression<Func<Role, bool>>? predicate = default, bool asNoTracking = false);
}
