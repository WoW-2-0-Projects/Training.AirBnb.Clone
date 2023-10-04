using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Infrastructure.Services.ListingServices;

namespace Backend_Project.Infrastructure.CompositionServices
{
    public class AmenitiesManagementService : IAmenitiesManagementService
    {
        private readonly IEntityBaseService<Amenity> _amenityService;
        private readonly IEntityBaseService<AmenityCategory> _amenityCategoryService;
        private readonly IEntityBaseService<ListingAmenities> _listingAmenitiesService;
        private readonly IEntityBaseService<Listing> _listingService;

        public AmenitiesManagementService(IEntityBaseService<Amenity> amenityService,
            IEntityBaseService<AmenityCategory>  amenityCategoryService, 
            IEntityBaseService<ListingAmenities> listingAmenitiesService,
            IEntityBaseService<Listing> listingService)
        {
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;   
            _listingAmenitiesService = listingAmenitiesService;
            _listingService = listingService;
        }
        public Amenity AddAmenity(Amenity amenity)
        {
            throw new NotImplementedException();
        }
        public Amenity DeleteAmenity(Amenity amenity)
        {
            throw new NotImplementedException();
        }

        public ListingAmenities AddListingAmenities(ListingAmenities listingAmenities)
        {
            throw new NotImplementedException();
        }

        public ListingAmenities AddListingAmenities(Guid listingId, List<Guid> amenitiesIds)
        {
            throw new NotImplementedException();
        }


        public ListingAmenities UpdateListingAmenities(ListingAmenities listingAmenities)
        {
            throw new NotImplementedException();
        }

        public ListingAmenities UpdateListingAmenities(Guid listingId, List<Guid> amenitiesIds)
        {
            throw new NotImplementedException();
        }
    }
}
