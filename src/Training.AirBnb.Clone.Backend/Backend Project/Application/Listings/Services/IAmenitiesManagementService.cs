using Backend_Project.Application.Amenities;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Listings.Services;

public interface IAmenitiesManagementService
{
    // Amenitie's methods
    public ValueTask<AmenityDto> AddAmenity(AmenityDto amenity, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    public ValueTask<AmenityDto> UpdateAmenityAsycn(AmenityDto amenityDto, bool saveChanges = true, CancellationToken cancellationToken = default);
    
    public ValueTask<AmenityDto> DeleteAmenityAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    // AmenitiesCategorie's methods
    public ValueTask<ICollection<Amenity>> GetAmenitiesByCategoryId(Guid amenityCategoryId, CancellationToken cancellationToken = default);
    
    public ValueTask<AmenityCategoryDto> DeleteAmenitiesCategory(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

    // ListingAmenitesMethods
    public ValueTask<ListingAmenitiesDto> AddListingAmenitiesAsync(ListingAmenitiesDto listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default);
}