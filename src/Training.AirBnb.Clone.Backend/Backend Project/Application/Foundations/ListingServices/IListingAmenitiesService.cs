using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingAmenitiesService
{
    IQueryable<ListingAmenities> Get(Expression<Func<ListingAmenities, bool>> predicate);

    ValueTask<ICollection<ListingAmenities>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingAmenities> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingAmenities> CreateAsync(ListingAmenities listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingAmenities> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingAmenities> DeleteAsync(ListingAmenities listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default);
}