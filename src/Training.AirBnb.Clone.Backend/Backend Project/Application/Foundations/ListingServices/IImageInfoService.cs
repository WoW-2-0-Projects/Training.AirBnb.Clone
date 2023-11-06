using Backend_Project.Domain.Entities;
using System.Linq.Expressions;

namespace Backend_Project.Application.Foundations.ListingServices;

public interface IImageInfoService
{
    IQueryable<ImageInfo> Get(Expression<Func<ImageInfo, bool>> predicate);

    ValueTask<ICollection<ImageInfo>> GetAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    ValueTask<ImageInfo> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    ValueTask<ImageInfo> CreateAsync(ImageInfo listingImage, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ImageInfo> DeleteAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<ImageInfo> DeleteAsync(ImageInfo listingImage, bool saveChanges = true, CancellationToken cancellationToken = default);
}