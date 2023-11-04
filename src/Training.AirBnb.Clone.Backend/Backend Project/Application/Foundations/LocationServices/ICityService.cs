using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface ICityService
{
    IQueryable<City> Get(Expression<Func<City, bool>> predicate);

    ValueTask<ICollection<City>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<City> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<City> CreateAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<City> UpdateAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<City> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<City> DeleteAsync(City city, bool saveChanges = true, CancellationToken cancellationToken = default);
}