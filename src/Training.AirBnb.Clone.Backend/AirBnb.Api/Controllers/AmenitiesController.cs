using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenitiesController : ControllerBase
    {
        private readonly IEntityBaseService<Amenity> _amenityService;
        private readonly IAmenitiesManagementService _amenitiesManagementService;
        private readonly IEntityBaseService<AmenityCategory> _amenityCategoryService;
        public AmenitiesController(IEntityBaseService<Amenity> amenityService,
            IAmenitiesManagementService amenitiesManagementService,
            IEntityBaseService<AmenityCategory> amenityCategoryService)
        {
            _amenitiesManagementService = amenitiesManagementService;
            _amenityService = amenityService;
            _amenityCategoryService = amenityCategoryService;
        }

        [HttpPost("amenities")]
        public async Task<IActionResult> PostAsync(Amenity amenity)
        {
            return Ok(await _amenitiesManagementService.AddAmenity(amenity));
        }

        [HttpPost("amenitiescategory")]
        public async Task<IActionResult> PostAsyncAmenityCategory(AmenityCategory amenityCategory)
        {
            return Ok(await _amenityCategoryService.CreateAsync(amenityCategory));
        }

        //[HttpPost]
        //[HttpGet("amenities/id")]
        //public async Task<IActionResult> GetByIdAsync(Guid id)
        //{
        //    return await 
        //}

    }
}
