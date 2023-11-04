using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings;
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
        public AmenitiesController(IAmenityService amenityService,
            IAmenitiesManagementService amenitiesManagementService,
            IAmenityCategoryService amenityCategoryService)
        {
            _amenitiesManagementService = amenitiesManagementService;
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;
        }

#region GetAmenties
        [HttpGet("amenities")]
        public IActionResult GetAllAmenities()
        {
            var result =  _amenityService.Get(amenties => true);

            return result.Any() ? Ok(result) : NotFound();
        }
#endregion

#region GetByIdAmenty
        [HttpGet("amenities/{amenityId:guid}")]
        public async ValueTask<IActionResult> GetAmentiesById(Guid amenityId, CancellationToken cancellationToken = default)
            =>  Ok(await _amenityService.GetByIdAsync(amenityId,cancellationToken));
        #endregion

#region GetAmenityByCategoryId
        [HttpGet("amenitiesByCategoryId/{amenityCategoryId:guid}")]
        public async ValueTask<IActionResult> GetAmenitiesByCategoryId(Guid amenityCategoryId, CancellationToken cancellationToken = default)
        {
           var result =  await _amenitiesManagementService.GetAmenitiesByCategoryId(amenityCategoryId, cancellationToken);

            return result.Any() ? Ok(result) : NotFound();
        }
        #endregion

#region AddAmenites
        [HttpPost("amenities")]
        public async Task<IActionResult> AddAmenityAsync([FromBody] Amenity amenity, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return Ok(await _amenitiesManagementService.AddAmenity(amenity, saveChanges, cancellationToken));
        }
        #endregion

#region UpdateAmenity
        [HttpPut("amenities/update")]
        public async Task<IActionResult> UpdateAmenityAsync(Amenity amenity, bool saveChanges = true , CancellationToken cancellationToken = default)
        {
            return Ok(await _amenitiesManagementService.UpdateAmenityAsycn(amenity, saveChanges, cancellationToken));
        }
        
#endregion

#region DeleteAmenties
        [HttpDelete("amenities/Delete/{Id:guid}")]
        public async Task<IActionResult> DeleteAmenityAsync([FromRoute] Guid Id )
        {
            await _amenitiesManagementService.DeleteAmenityAsync(Id);

            return NoContent();
        }
#endregion

#region GetAmenityCategories
        [HttpGet("amenitiesCategory")]
        public IActionResult GetAllAmeniitiesCategory()
        {
            var result = _amenityCategoryService.Get(amenityCategory => true);

            return result.Any() ? Ok(result) : NoContent();
        }

#endregion

#region GetAmenityCategoryById
        [HttpGet("amenitiesCategory/{amenitiesCategoryId:guid}")]
        public async ValueTask<IActionResult> GetAmenitiesCategoryById([FromRoute] Guid amenitiesCategoryId, CancellationToken cancellationToken)
            => Ok(await _amenityCategoryService.GetByIdAsync(amenitiesCategoryId, cancellationToken));

#endregion

#region AddAmenitiesCategory
        [HttpPost("amenitiesCategory")]
        public async Task<IActionResult> PostAsyncAmenityCategory([FromBody] AmenityCategory amenityCategory)
        {
            return Ok(await _amenityCategoryService.CreateAsync(amenityCategory));
        }
        #endregion

#region UpdateAmenitiesCategory
        [HttpPut("amenitiesCategory")]
        public async Task<IActionResult> UpdateAmenitiesCategoryAsync([FromBody] AmenityCategory amenityCategory, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            return Ok(await _amenityCategoryService.UpdateAsync(amenityCategory, saveChanges, cancellationToken));
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
        public async Task<IActionResult> AddListingAmenitiesAsync(ListingAmenities listingAmenities)
        {
           return Ok(await _amenitiesManagementService.AddListingAmenitiesAsync(listingAmenities));
        }
#endregion

    }
}