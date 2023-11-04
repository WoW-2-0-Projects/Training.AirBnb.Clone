using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IAmenityCategoryService
{
    IQueryable<AmenityCategory> Get(Expression<Func<AmenityCategory, bool>> predicate);

    ValueTask<ICollection<AmenityCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<AmenityCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<AmenityCategory> CreateAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<AmenityCategory> UpdateAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<AmenityCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<AmenityCategory> DeleteAsync(AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default);
}