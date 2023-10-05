using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
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
            IEntityBaseService<AmenityCategory> amenityCategoryService,
            IEntityBaseService<ListingAmenities> listingAmenitiesService,
            IEntityBaseService<Listing> listingService)
        {
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;
            _listingAmenitiesService = listingAmenitiesService;
            _listingService = listingService;
        }

#region Amenitie's methods
        public async ValueTask<Amenity> AddAmenity(Amenity amenity, bool saveChanges = true, 
            CancellationToken cancellation = default)
        {
            await _amenityCategoryService.GetByIdAsync(amenity.CategoryId);

            return await _amenityService.CreateAsync(amenity);
        }

        public async ValueTask<Amenity> UpdateAmenityAsycn(Amenity amenity, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            await _amenityCategoryService.GetByIdAsync(amenity.CategoryId);
            
            return await _amenityService.UpdateAsync(amenity, saveChanges, cancellationToken);
        }
        
        public async ValueTask<Amenity> DeleteAmenityAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var amenity = await _amenityService.GetByIdAsync(id, cancellationToken);

            var listingAmenities =  _listingAmenitiesService
                .Get(la => la.AmenityId.Equals(id)).ToList();

            if (listingAmenities is not null)
                throw new EntityNotDeletableException<Amenity>("this amenity not Deletable");

            return await _amenityService.DeleteAsync(amenity);
        }

        #endregion

        // AmenitiesCategorie's methods
        public ValueTask<ICollection<Amenity>> GetAmenitiesByCategoryId( Guid amenityCategoryId, CancellationToken cancellationToken = default)
                =>  new ValueTask<ICollection<Amenity>>(
                 _amenityService.Get(ac => ac.CategoryId.Equals(amenityCategoryId)).ToList());

        public async ValueTask<AmenityCategory> DeleteAmenitiesCategory(Guid id, bool saveChanges, CancellationToken cancellationToken = default)
        {
            var amenitiesCategory = await _amenityCategoryService.GetByIdAsync(id, cancellationToken);

            var amenities = _amenityService.Get(a => a.CategoryId.Equals(id)).ToList();

            if (amenities is not null)
                throw new EntityNotDeletableException<AmenityCategory>("This Category not Deletable");

            return await _amenityCategoryService.DeleteAsync(amenitiesCategory, saveChanges, cancellationToken);
        }


        // ListingAmenites methods 
        public async ValueTask<ListingAmenities> AddListingAmenitiesAsync(ListingAmenities listingAmenities,
            bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            //await _listingService.GetByIdAsync(listingAmenities.ListingId);
            await _amenityService.GetByIdAsync(listingAmenities.AmenityId);

            return await _listingAmenitiesService.CreateAsync(listingAmenities, saveChanges, cancellationToken);
        }

            // bu method to'liq ishlashi uchun ListingModel tayyor bo'lishi kerak

        // Listing tayyor bo'lganidan so'ng ishlatiladi
        //public ListingAmenities AddListingAmenities(Guid listingId, List<Guid> amenitiesIds)
        //{

        //    throw new NotImplementedException();
        //}


        //public ListingAmenities UpdateListingAmenities(ListingAmenities listingAmenities)
        //{
        //    throw new NotImplementedException();
        //}

        //public ListingAmenities UpdateListingAmenities(Guid listingId, List<Guid> amenitiesIds)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
