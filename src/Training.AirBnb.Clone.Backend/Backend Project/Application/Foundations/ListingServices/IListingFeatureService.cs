using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingFeatureService
{
    IQueryable<ListingFeature> Get(Expression<Func<ListingFeature, bool>> predicate);

    ValueTask<ICollection<ListingFeature>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingFeature> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingFeature> CreateAsync(ListingFeature listingFeature, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingFeature> UpdateAsync(ListingFeature listingFeature, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingFeature> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingFeature> DeleteAsync(ListingFeature listingFeature, bool saveChanges = true, CancellationToken cancellationToken = default);
}