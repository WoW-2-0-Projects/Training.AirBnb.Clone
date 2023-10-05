using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;

public interface IListingCategoryDetailsService
{
    ValueTask<ListingCategory> DeleteCategoryAsync(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ICollection<ListingFeatureOption>> GetFeatureOptionsByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    ValueTask<ListingFeatureOption> DeleteFeatureOptionAsync(Guid featureOptionId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeature> AddListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeature> UpdateListingFeatureAsync(ListingFeature feature, bool saveChanges = true, CancellationToken cancellationToken = default);
    ICollection<ListingFeature> GetListingFeaturesByOptionId(Guid listingFeatureOptionId);
    ValueTask<ListingFeature> DeleteListingFeatureAsync(Guid featureId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingCategoryFeatureOption> AddListingCategoryFeatureOptionAsync(ListingCategoryFeatureOption relation, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> AddListingCategoryFeatureOptionsAsync(Guid categoryId, List<Guid> featureOptions, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> UpdateListingCategoryFeatureOptionsAsync(Guid categoryId, List<Guid> updatedFeatureOptions, bool saveChanges = true, CancellationToken cancellationToken = default);
}