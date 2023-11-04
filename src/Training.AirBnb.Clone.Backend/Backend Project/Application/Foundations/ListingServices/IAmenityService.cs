using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IAmenityService
{
    IQueryable<Amenity> Get(Expression<Func<Amenity, bool>> predicate);

    ValueTask<ICollection<Amenity>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<Amenity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<Amenity> CreateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Amenity> UpdateAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Amenity> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<Amenity> DeleteAsync(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);
}