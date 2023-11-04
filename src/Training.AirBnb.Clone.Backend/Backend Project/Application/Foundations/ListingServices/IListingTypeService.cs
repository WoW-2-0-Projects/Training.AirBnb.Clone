using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingTypeService
{
    IQueryable<ListingType> Get(Expression<Func<ListingType, bool>> predicate);

    ValueTask<ICollection<ListingType>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingType> CreateAsync(ListingType listingType, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingType> UpdateAsync(ListingType listingType, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingType> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingType> DeleteAsync(ListingType listingType, bool saveChanges = true, CancellationToken cancellationToken = default);
}