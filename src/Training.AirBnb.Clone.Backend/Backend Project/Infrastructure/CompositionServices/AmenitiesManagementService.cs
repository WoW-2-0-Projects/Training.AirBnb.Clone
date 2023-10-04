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
        public async ValueTask<Amenity> AddAmenity(Amenity amenity, bool saveChanges = true, CancellationToken cancellation = default)
        {
            await _amenityCategoryService.GetByIdAsync(amenity.CategoryId);

            return await _amenityService.CreateAsync(amenity);
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
