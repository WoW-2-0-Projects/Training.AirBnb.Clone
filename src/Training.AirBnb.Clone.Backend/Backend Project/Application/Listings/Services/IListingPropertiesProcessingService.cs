using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Listings.Services;

public interface IListingPropertiesProcessingService
{
    ValueTask<ListingPropertyType> CreateListingPropertyTypeAsync(ListingPropertyType propertyType);

    ValueTask<ListingPropertyType> UpdateListingPropertyTypeAsync(ListingPropertyType propertyType);

    ValueTask<ListingPropertyType> DeleteListingPropertyTypeAsync(ListingPropertyType propertyType);

    ListingProperty CreateListingProperty(ListingProperty property);

    ListingProperty UpdateListingProperty(ListingProperty property);

    ListingProperty DeleteListingProperty(ListingProperty property);
}