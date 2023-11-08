using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.ListingCategoryDetails.Services;

public interface IListingCategoryDetailsService
{
    ValueTask<ListingCategoryDto> DeleteCategoryAsync(Guid categoryId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ICollection<ListingTypeDto>> GetListingTypesByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
    ValueTask<ListingTypeDto> DeleteListingTypeAsync(Guid typeId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeatureDto> AddListingFeatureAsync(ListingFeatureDto featureDto, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingFeatureDto> UpdateListingFeatureAsync(ListingFeatureDto featureDto, bool saveChanges = true, CancellationToken cancellationToken = default);
    ICollection<ListingFeatureDto> GetListingFeaturesByTypeId(Guid listingTypeId);
    ValueTask<ListingFeatureDto> DeleteListingFeatureAsync(Guid featureId, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<ListingCategoryTypeDto> AddListingCategoryTypeAsync(ListingCategoryTypeDto relation, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> AddListingCategoryTypesAsync(Guid categoryId, List<Guid> listingTypes, bool saveChanges = true, CancellationToken cancellationToken = default);
    ValueTask<bool> UpdateListingCategoryTypesAsync(Guid categoryId, List<Guid> updatedListingTypes, bool saveChanges = true, CancellationToken cancellationToken = default);
}