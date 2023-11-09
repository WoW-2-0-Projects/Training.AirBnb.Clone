using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Services;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.CompositionServices;

public class ListingPropertiesProcessingService : IListingPropertiesProcessingService
{
    private readonly IListingPropertyTypeService _propertyTypeService;
    private readonly IListingPropertyService _propertyService;
    private readonly IListingService _listingService;
    private readonly IListingCategoryTypeService _listingCategoryTypeService;
    private readonly IListingFeatureService _listingFeatureService;

    public ListingPropertiesProcessingService(
        IListingPropertyTypeService propertyTypeService,
        IListingPropertyService propertyService,
        IListingService listingService,
        IListingCategoryTypeService listingCategoryTypeService,
        IListingFeatureService listingFeatureService)
    {
        _propertyTypeService = propertyTypeService;
        _propertyService = propertyService;
        _listingService = listingService;
        _listingCategoryTypeService = listingCategoryTypeService;
        _listingFeatureService = listingFeatureService;
    }

    public async ValueTask<ListingPropertyType> CreateListingPropertyTypeAsync(ListingPropertyType propertyType)
    {
        ValidatePropertyType(propertyType);

        var createdPropertyType = await _propertyTypeService.CreateAsync(propertyType);

        return createdPropertyType;
    }

    public async ValueTask<ListingPropertyType> UpdateListingPropertyTypeAsync(ListingPropertyType propertyType)
    {
        ValidatePropertyType(propertyType);

        var updatedPropertyType = await _propertyTypeService.UpdateAsync(propertyType);

        return updatedPropertyType;
    }

    public async ValueTask<ListingPropertyType> DeleteListingPropertyTypeAsync(ListingPropertyType propertyType)
    {
        var listing = _listingService.Get(self => true).FirstOrDefault(self => self.PropertyTypeId == propertyType.Id);

        if (listing is not null)
            throw new EntityNotDeletableException<ListingPropertyType>("There is an active listing which has this property type.");
        
        return await _propertyTypeService.DeleteAsync(propertyType);
    }

    public ListingProperty CreateListingProperty(ListingProperty property)
    {
        throw new NotImplementedException();
    }

    public ListingProperty UpdateListingProperty(ListingProperty property)
    {
        throw new NotImplementedException();
    }

    public ListingProperty DeleteListingProperty(ListingProperty property)
    {
        throw new NotImplementedException();
    }

    private void ValidatePropertyType(ListingPropertyType propertyType)
    {
        var categoryTypes = _listingCategoryTypeService
            .Get(category => true)
            .Where(category => category.ListingCategoryId == propertyType.CategoryId);

        if (!categoryTypes.Any())
            throw new EntityNotFoundException<ListingCategory>("Category not found!");

        if (propertyType.TypeId is not null)
            _ = categoryTypes
                .FirstOrDefault(categoryType => categoryType.ListingCategoryId == propertyType.CategoryId
                && categoryType.ListingTypeId == propertyType.TypeId)
                ?? throw new EntityNotFoundException<ListingType>("Listing type not found!");
    }
}