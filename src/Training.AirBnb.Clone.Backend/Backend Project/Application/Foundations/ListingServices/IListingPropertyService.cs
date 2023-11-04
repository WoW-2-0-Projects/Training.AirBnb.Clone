using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingPropertyService
{
    IQueryable<ListingProperty> Get(Expression<Func<ListingProperty, bool>> predicate);

    ValueTask<ICollection<ListingProperty>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingProperty> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingProperty> CreateAsync(ListingProperty listingProperty, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingProperty> UpdateAsync(ListingProperty listingProperty, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingProperty> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingProperty> DeleteAsync(ListingProperty listingProperty, bool saveChanges = true, CancellationToken cancellationToken = default);
}