using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces
{
    public interface IAmenitiesManagementService
    {
        public Amenity AddAmenity(Amenity amenity);
        public Amenity DeleteAmenity(Amenity amenity);

        public ListingAmenities AddListingAmenities(ListingAmenities listingAmenities);
        public ListingAmenities AddListingAmenities(Guid listingId, List<Guid> amenitiesIds);
        public ListingAmenities UpdateListingAmenities(ListingAmenities listingAmenities);

        public ListingAmenities UpdateListingAmenities(Guid listingId, List<Guid> amenitiesIds);

    }
}
