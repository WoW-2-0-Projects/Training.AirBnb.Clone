using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Listings.Services;

public interface IListingPropertiesProcessingService
{
    ValueTask<ListingPropertyType> CreateListingPropertyTypeAsync(ListingPropertyType propertyType);

    ValueTask<ListingPropertyType> UpdateListingPropertyTypeAsync(ListingPropertyType propertyType);

    ValueTask<ListingPropertyType> DeleteListingPropertyTypeAsync(ListingPropertyType propertyType);

    ValueTask<bool> AddListingProperties(ICollection<ListingProperty> properties);

    ValueTask<bool> UpdateListingProperty(ICollection<ListingProperty> properties);
}