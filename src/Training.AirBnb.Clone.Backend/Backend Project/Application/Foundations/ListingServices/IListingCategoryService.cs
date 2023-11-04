using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingCategoryService
{
    IQueryable<ListingCategory> Get(Expression<Func<ListingCategory, bool>> predicate);

    ValueTask<ICollection<ListingCategory>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingCategory> CreateAsync(ListingCategory listingCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingCategory> UpdateAsync(ListingCategory listingCategory, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingCategory> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingCategory> DeleteAsync(ListingCategory listingCategory, bool saveChanges = true, CancellationToken cancellationToken = default);
}