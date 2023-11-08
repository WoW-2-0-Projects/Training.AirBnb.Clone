using AutoMapper;
using Backend_Project.Application.Amenities.Dtos;
using Backend_Project.Application.Amenities.Services;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;

namespace Backend_Project.Infrastructure.CompositionServices
{
    public class AmenitiesManagementService : IAmenitiesManagementService
    {
        private readonly IMapper _mapper;
        private readonly IAmenityService _amenityService;
        private readonly IAmenityCategoryService _amenityCategoryService;
        private readonly IListingAmenitiesService _listingAmenitiesService;

        public AmenitiesManagementService(IAmenityService amenityService,
            IAmenityCategoryService amenityCategoryService,
            IListingAmenitiesService listingAmenitiesService,
            IMapper mapper)
        {
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;
            _listingAmenitiesService = listingAmenitiesService;
            _mapper = mapper;
        }

        #region Amenitie's methods
        public async ValueTask<AmenityDto> AddAmenity(AmenityDto amenityDto, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            var amenity = _mapper.Map<Amenity>(amenityDto);

            await _amenityCategoryService.GetByIdAsync(amenity.CategoryId, cancellationToken);

            return _mapper.Map<AmenityDto>(await _amenityService.CreateAsync(amenity, saveChanges, cancellationToken));
        }

        public async ValueTask<AmenityDto> UpdateAmenityAsycn(AmenityDto amenityDto, bool saveChanges = true,
            CancellationToken cancellationToken = default)
        {
            var amenity = _mapper.Map<Amenity>(amenityDto);

            await _amenityCategoryService.GetByIdAsync(amenityDto.CategoryId, cancellationToken);

            return _mapper.Map<AmenityDto>(await _amenityService.UpdateAsync(amenity, saveChanges, cancellationToken));
        }

        public async ValueTask<AmenityDto> DeleteAmenityAsync(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var amenity = await _amenityService.GetByIdAsync(id, cancellationToken);

            var listingAmenities = _listingAmenitiesService
                .Get(la => la.AmenityId.Equals(id));

            if (listingAmenities.Any())
                throw new EntityNotDeletableException<Amenity>("this amenity not Deletable");

            return _mapper.Map<AmenityDto>(await _amenityService.DeleteAsync(amenity, saveChanges, cancellationToken));
        }

        #endregion

        // AmenitiesCategorie's methods
        public ValueTask<ICollection<AmenityDto>> GetAmenitiesByCategoryId(Guid amenityCategoryId, CancellationToken cancellationToken = default)
                => new(
                 _amenityService.Get(ac => ac.CategoryId.Equals(amenityCategoryId))
                    .Select(ac => _mapper.Map<AmenityDto>(ac))
                    .ToList());

        public async ValueTask<AmenityCategoryDto> DeleteAmenitiesCategory(Guid id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            var amenitiesCategoryDto = _mapper.Map<AmenityCategoryDto>(await _amenityCategoryService.GetByIdAsync(id, cancellationToken));

            var amenities = _amenityService.Get(a => a.CategoryId.Equals(amenitiesCategoryDto.Id));

            if (amenities.Any())
                throw new EntityNotDeletableException<AmenityCategory>("This Category not Deletable");

            return _mapper.Map<AmenityCategoryDto>(await _amenityCategoryService.DeleteAsync(_mapper.Map<AmenityCategory>(amenitiesCategoryDto), saveChanges, cancellationToken));
        }


        // ListingAmenites methods 
        public async ValueTask<ListingAmenitiesDto> AddListingAmenitiesAsync(ListingAmenitiesDto listingAmenities,
            bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            await _amenityService.GetByIdAsync(listingAmenities.AmenityId, cancellationToken);

            return _mapper.Map<ListingAmenitiesDto>( await _listingAmenitiesService.CreateAsync(_mapper.Map<ListingAmenities>( listingAmenities), saveChanges, cancellationToken));
        }
    }
}