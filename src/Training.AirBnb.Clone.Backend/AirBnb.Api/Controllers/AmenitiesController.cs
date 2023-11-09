using AutoMapper;
using Backend_Project.Application.Amenities.Dtos;
using Backend_Project.Application.Amenities.Services;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IAmenityService _amenityService;
        private readonly IAmenitiesManagementService _amenitiesManagementService;
        private readonly IAmenityCategoryService _amenityCategoryService;
        private readonly IMapper _mapper;

        public AmenitiesController(IAmenityService amenityService,
            IAmenitiesManagementService amenitiesManagementService,
            IAmenityCategoryService amenityCategoryService,
            IMapper mapper)
        {
            _amenitiesManagementService = amenitiesManagementService;
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;
            _mapper = mapper;
        }

        #region GetAmenties
        [HttpGet("amenities")]
        public IActionResult GetAllAmenities()
        {
            var result = _amenityService.Get(amenties => true)
                .Select(a => _mapper.Map<AmenityDto>(a));

            return result.Any() ? Ok(result) : NotFound();
        }
        #endregion

        #region GetByIdAmenty
        [HttpGet("amenities/{amenityId:guid}")]
        public async ValueTask<IActionResult> GetAmentiesById(Guid amenityId, CancellationToken cancellationToken = default)
            => Ok(_mapper.Map<AmenityDto>(await _amenityService.GetByIdAsync(amenityId, cancellationToken)));
        #endregion

        #region GetAmenityByCategoryId
        [HttpGet("amenitiesByCategoryId/{amenityCategoryId:guid}")]
        public async ValueTask<IActionResult> GetAmenitiesByCategoryId(Guid amenityCategoryId, CancellationToken cancellationToken = default)
        {
            var resultAmenities = await _amenitiesManagementService.GetAmenitiesByCategoryId(amenityCategoryId, cancellationToken);

            var result = resultAmenities.Select(result => _mapper.Map<AmenityDto>(result));

            return result.Any() ? Ok(result) : NotFound();
        }
        #endregion

        #region AddAmenites
        [HttpPost("amenities")]
        public async Task<IActionResult> AddAmenityAsync([FromBody] AmenityDto amenityDto, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return Ok(_mapper.Map<AmenityDto>( await _amenitiesManagementService.AddAmenity(_mapper.Map<Amenity>(amenityDto), saveChanges, cancellationToken)));
        }
        #endregion.

        #region UpdateAmenity
        [HttpPut("amenities/update")]
        public async Task<IActionResult> UpdateAmenityAsync(AmenityDto amenityDto, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return Ok(_mapper.Map<AmenityDto>( await _amenitiesManagementService.UpdateAmenityAsycn(_mapper.Map<Amenity>(amenityDto), saveChanges, cancellationToken)));
        }

        #endregion

        #region DeleteAmenties
        [HttpDelete("amenities/Delete/{Id:guid}")]
        public async Task<IActionResult> DeleteAmenityAsync([FromRoute] Guid Id)
        {
            await _amenitiesManagementService.DeleteAmenityAsync(Id);

            return NoContent();
        }
        #endregion

        #region GetAmenityCategories
        [HttpGet("amenitiesCategory")]
        public IActionResult GetAllAmeniitiesCategory()
        {
            var result = _amenityCategoryService.Get(amenityCategory => true)
                .Select(ac => _mapper.Map<AmenityCategoryDto>(ac)).ToList();

            return result.Any() ? Ok(result) : NoContent();
        }

        #endregion

        #region GetAmenityCategoryById
        [HttpGet("amenitiesCategory/{amenitiesCategoryId:guid}")]
        public async ValueTask<IActionResult> GetAmenitiesCategoryById([FromRoute] Guid amenitiesCategoryId, CancellationToken cancellationToken)
            => Ok(_mapper.Map<AmenityCategoryDto>(await _amenityCategoryService.GetByIdAsync(amenitiesCategoryId, cancellationToken)));

        #endregion

        #region AddAmenitiesCategory
        [HttpPost("amenitiesCategory")]
        public async Task<IActionResult> PostAsyncAmenityCategory([FromBody] AmenityCategoryDto amenityCategory)
        {
            return Ok(_mapper.Map<AmenityCategoryDto>( await _amenityCategoryService.CreateAsync(_mapper.Map<AmenityCategory>(amenityCategory))));
        }
        #endregion

        #region UpdateAmenitiesCategory
        [HttpPut("amenitiesCategory")]
        public async Task<IActionResult> UpdateAmenitiesCategoryAsync([FromBody] AmenityCategoryDto amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return Ok(_mapper.Map<AmenityCategoryDto>( await _amenityCategoryService.UpdateAsync(_mapper.Map<AmenityCategory>(amenityCategory), saveChanges, cancellationToken)));
        }
        #endregion

        #region DeleteAmenitiesCategory
        [HttpDelete("amenitiesCategory/{Id:guid}")]
        public async Task<IActionResult> DeleteAmenitiesCategory([FromRoute] Guid Id)
        {
            await _amenitiesManagementService.DeleteAmenitiesCategory(Id);

            return NoContent();
        }
        #endregion

        #region AddListingAmenities
        [HttpPost("listingAmenities")]
        public async Task<IActionResult> AddListingAmenitiesAsync(ListingAmenitiesDto listingAmenities)
        {
            return Ok(_mapper.Map<ListingAmenitiesDto>( await _amenitiesManagementService.AddListingAmenitiesAsync(_mapper.Map<ListingAmenities>( listingAmenities))));
        }
        #endregion

    }
}