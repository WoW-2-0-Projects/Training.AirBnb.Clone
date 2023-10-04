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
        
        public AmenitiesController(IEntityBaseService<Amenity> amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpPost("amenities")]
        public async Task<IActionResult> PostAsyc(Amenity amenity)
        {
            return Ok( await _amenityService.CreateAsync(amenity) );
        }

        //[HttpGet("amenities/id")]
        //public async Task<IActionResult> GetByIdAsync(Guid id)
        //{
        //    return await 
        //}

    }
}
