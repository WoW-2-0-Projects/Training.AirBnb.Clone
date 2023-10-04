using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;

public interface IListingCategoryDetailsService
{
    ValueTask<ListingCategory> DeleteCategoryAsync(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeatureOption> DeleteFeatureOptionAsync(Guid featureOptionId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeature> AddListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeature> UpdateListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default);
}