using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface ILocationService
{
    IQueryable<Location> Get(Expression<Func<Location, bool>> predicate);

    ValueTask<ICollection<Location>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Location> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Location> CreateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location> UpdateAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Location> DeleteAsync(Location location, bool saveChanges = true, CancellationToken cancellationToken = default);
}