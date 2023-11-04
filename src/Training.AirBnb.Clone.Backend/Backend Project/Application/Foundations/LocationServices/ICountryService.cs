using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.LocationServices;

public interface ICountryService
{
    IQueryable<Country> Get(Expression<Func<Country, bool>> predicate);

    ValueTask<ICollection<Country>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Country> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Country> CreateAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Country> UpdateAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Country> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Country> DeleteAsync(Country country, bool saveChanges = true, CancellationToken cancellationToken = default);
}