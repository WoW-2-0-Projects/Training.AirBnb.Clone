using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingCategoryTypeService
{
    IQueryable<ListingCategoryType> Get(Expression<Func<ListingCategoryType, bool>> predicate);

    ValueTask<ICollection<ListingCategoryType>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingCategoryType> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingCategoryType> CreateAsync(ListingCategoryType listingCategoryType, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingCategoryType> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingCategoryType> DeleteAsync(ListingCategoryType listingCategoryType, bool saveChanges = true, CancellationToken cancellationToken = default);
}