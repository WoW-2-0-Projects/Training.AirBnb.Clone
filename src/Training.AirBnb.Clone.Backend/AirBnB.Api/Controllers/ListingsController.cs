using AirBnB.Api.Models.DTOs;
using AirBnB.Api.Models.DTOs.Listings;
using AirBnB.Application.Listings.Models;
using AirBnB.Application.Listings.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AirBnB.Domain.Common.Query;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using Microsoft.EntityFrameworkCore;
[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingService listingService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination,
        CancellationToken cancellationToken)
    {
        var specification = filterPagination.ToQueryPagination(true).ToQuerySpecification();
        var result = await listingService
            .GetAsync(specification, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }
    
    [HttpGet("categories")]
    public async ValueTask<IActionResult> GetListingCategories(
        [FromServices] IListingCategoryService listingCategoryService, 
        CancellationToken cancellationToken = default)
    {
        var result = await listingCategoryService
            .GetAsync(new ListingCategoryFilter().ToQuerySpecification(), cancellationToken);

        return result.Any() ? Ok(mapper.Map<List<ListingCategoryDto>>(result)) : NoContent();
    }
    [HttpGet("{listingId:guid}")]
    public async ValueTask<IActionResult> GetListingById([FromRoute]Guid listingId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var result = await listingService.GetByIdAsync(listingId, true, cancellationToken);

        return result is not null ? Ok(mapper.Map<ListingDto>(result)) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateAsync([FromBody] ListingDto listingDto,
        CancellationToken cancellationToken)
    {
        var listing = mapper.Map<Listing>(listingDto);
        var result = await listingService.CreateAsync(listing, true, cancellationToken);

        return Ok(mapper.Map<ListingDto>(result));
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateAsync([FromBody] ListingDto listingDto, 
        CancellationToken cancellationToken)
    {
        var listing = mapper.Map<Listing>(listingDto);
        var result = await listingService.UpdateAsync(listing, true, cancellationToken);

        return Ok(mapper.Map<ListingDto>(result));
    }

    [HttpDelete("{listingId:guid}")]
    public async ValueTask<IActionResult> DeleteListingById([FromRoute]Guid listingId,
        CancellationToken cancellationToken)
    {
        var listing = await listingService.DeleteByIdAsync(listingId, true, cancellationToken);

        return listing is not null ? Ok(mapper.Map<ListingDto>(listing)) : NotFound();
    }
}