using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingRatingService
{
    IQueryable<ListingRating> Get(Expression<Func<ListingRating, bool>> predicate);

    ValueTask<ICollection<ListingRating>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingRating> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingRating> CreateAsync(ListingRating listingRating, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRating> UpdateAsync(ListingRating listingRating, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRating> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingRating> DeleteAsync(ListingRating listingRating, bool saveChanges = true, CancellationToken cancellationToken = default);
}