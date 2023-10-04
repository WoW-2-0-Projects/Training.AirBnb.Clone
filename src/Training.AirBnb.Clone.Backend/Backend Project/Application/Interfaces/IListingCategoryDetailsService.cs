using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;

public interface IListingCategoryDetailsService
{
    ValueTask<ListingCategory> DeleteCategoryAsync(Guid categoryId);
    ValueTask<ListingFeatureOption> DeleteFeatureOptionAsync(Guid featureOptionId);
    ValueTask<ListingFeature> AddListingFeatureAsync(ListingFeature feature);
    ValueTask<ListingFeature> UpdateListingFeatureAsync(ListingFeature feature);
}