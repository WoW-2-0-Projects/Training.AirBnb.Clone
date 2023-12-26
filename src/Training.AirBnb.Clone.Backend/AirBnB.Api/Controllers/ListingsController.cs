using AirBnB.Api.Models.DTOs.Listings;
using AirBnB.Application.Listings.Models;
using AirBnB.Application.Listings.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IMapper mapper) : ControllerBase
{
    [HttpGet("categories")]
    public async ValueTask<IActionResult> GetListingCategories(
        [FromServices] IListingCategoryService listingCategoryService, 
        CancellationToken cancellationToken = default)
    {
        var result = await listingCategoryService
            .GetAsync(new ListingCategoryFilter().ToQuerySpecification(), cancellationToken);

        return result.Any() ? Ok(mapper.Map<List<ListingCategoryDto>>(result)) : NoContent();
    }
}