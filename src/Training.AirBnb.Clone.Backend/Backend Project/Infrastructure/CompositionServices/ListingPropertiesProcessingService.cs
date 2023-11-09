using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Services;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using Backend_Project.Domain.Extensions;

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

    public ValueTask<bool> AddListingProperties(ICollection<ListingProperty> properties)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<bool> UpdateListingProperty(ICollection<ListingProperty> properties)
    {
        var listingId = properties.First().ListingId;

        if (properties.Any(property => property.ListingId != listingId))
            throw new EntityValidationException<ListingProperty>("Not all properties are for the same listing.");

        var foundProperties = _propertyService
            .Get(property => property.ListingId == listingId);

        var (addedProperties, removedProperties) = foundProperties.GetAddedAndRemovedItemsBy(properties, key => key.Id);

        await RemoveListingProperties(removedProperties);

        var updatedProperties = foundProperties.IntersectBy(properties.Select(property => property.Id), key => key.Id);

        ValidateProperties(addedProperties.Concat(updatedProperties));

        await AddListingProperties(addedProperties);
        await UpdateListingProperties(updatedProperties);

        return true;
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

    private void ValidateProperties(IEnumerable<ListingProperty> properties)
    {
        var listingProperties = _listingFeatureService.Get(feature => true);

        foreach (var property in properties)
        {
            var feature = listingProperties.FirstOrDefault(feature => feature.Name == property.PropertyName);

            if (feature is null || property.PropertyCount > feature.MaxValue || property.PropertyCount < feature.MinValue)
                throw new EntityValidationException<ListingProperty>("Given listing property is not listed in the features!");
        }
    }

    private async ValueTask AddListingProperties(IEnumerable<ListingProperty> properties)
    {
        foreach (var property in properties)
            await _propertyService.CreateAsync(property);
    }

    private async ValueTask RemoveListingProperties(IEnumerable<ListingProperty> properties)
    {
        foreach (var property in properties)
            await _propertyService.DeleteAsync(property);
    }

    private async ValueTask UpdateListingProperties(IEnumerable<ListingProperty> properties)
    {
        foreach (var property in properties)
            await _propertyService.UpdateAsync(property);
    }
}