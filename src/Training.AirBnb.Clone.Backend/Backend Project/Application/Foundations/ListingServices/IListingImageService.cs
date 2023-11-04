using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IListingImageService
{
    IQueryable<ListingImage> Get(Expression<Func<ListingImage, bool>> predicate);

    ValueTask<ICollection<ListingImage>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ListingImage> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ListingImage> CreateAsync(ListingImage listingImage, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingImage> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ListingImage> DeleteAsync(ListingImage listingImage, bool saveChanges = true, CancellationToken cancellationToken = default);
}