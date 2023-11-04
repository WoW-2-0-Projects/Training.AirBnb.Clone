using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingPropertyTypeService
{
    IQueryable<ListingPropertyType> Get(Expression<Func<ListingPropertyType, bool>> predicate);

    ValueTask<ICollection<ListingPropertyType>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingPropertyType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingPropertyType> CreateAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingPropertyType> UpdateAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingPropertyType> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingPropertyType> DeleteAsync(ListingPropertyType listingPropertyType, bool saveChanges = true, CancellationToken cancellationToken = default);
}