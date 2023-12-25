using AirBnB.Domain.Entities;
using System.Linq.Expressions;

namespace AirBnB.Persistence.Repositories.Interfaces;

public interface IRoleRepository
{
    IQueryable<Role> Get(
        Expression<Func<Role, bool>>? predicate = default,
        bool asNoTracking = false
        );
}
