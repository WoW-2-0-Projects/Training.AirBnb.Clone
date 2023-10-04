using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces
{
    public interface IAmenitiesManagementService
    {
        public ValueTask<Amenity> AddAmenity(Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default);
        public Amenity DeleteAmenity(Amenity amenity);

        public ListingAmenities AddListingAmenities(ListingAmenities listingAmenities);
        public ListingAmenities AddListingAmenities(Guid listingId, List<Guid> amenitiesIds);
        public ListingAmenities UpdateListingAmenities(ListingAmenities listingAmenities);

        public ListingAmenities UpdateListingAmenities(Guid listingId, List<Guid> amenitiesIds);

    }
}
