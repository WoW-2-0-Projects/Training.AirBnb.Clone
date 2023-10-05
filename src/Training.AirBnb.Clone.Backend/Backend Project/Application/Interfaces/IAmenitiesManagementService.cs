using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces
{
    public interface IAmenitiesManagementService
    {
        // Amenitie's methods
        public ValueTask<Amenity> AddAmenity(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);
        public ValueTask<Amenity> UpdateAmenityAsycn(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);
        public ValueTask<Amenity> DeleteAmenityAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);

        // AmenitiesCategorie's methods
        public ValueTask<ICollection<Amenity>> GetAmenitiesByCategoryId(Guid amenityCategoryId, CancellationToken cancellationToken = default);
        public ValueTask<AmenityCategory> DeleteAmenitiesCategory(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default);
        
        // ListingAmenitesMethods
        public ValueTask<ListingAmenities> AddListingAmenitiesAsync(ListingAmenities listingAmenities, bool saveChanges = true, CancellationToken cancellationToken = default);
        //public ListingAmenities AddListingAmenities(Guid listingId, List<Guid> amenitiesIds);
        //public ListingAmenities UpdateListingAmenities(ListingAmenities listingAmenities);

        //public ListingAmenities UpdateListingAmenities(Guid listingId, List<Guid> amenitiesIds);
    }
}