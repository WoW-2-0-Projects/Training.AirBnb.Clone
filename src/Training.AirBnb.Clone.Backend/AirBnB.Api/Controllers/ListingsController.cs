using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Listings.Models;
using AirBnB.Application.Listings.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(IListingService listingService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination)
    {
        var result = listingService.Get(filterPagination, true);
        
        return new ValueTask<IActionResult>(Ok(mapper.Map<List<ListingDto>>(result)));
    }

    [HttpGet("category/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetListingByCategoryId([FromQuery] FilterPagination filterPagination,
        [FromRoute] Guid categoryId)
    {
        var listings = await listingService.GetByCategoryId(filterPagination, categoryId, true).ToListAsync();
        return listings.Count != 0 ? Ok(mapper.Map<List<ListingDto>>(listings)) : NoContent();
    }
    
    [HttpGet("{listingId:guid}")]
    public async ValueTask<IActionResult> GetListingById([FromRoute]Guid listingId, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        var result = await listingService.GetByIdAsync(listingId, true, cancellationToken);

        return result is not null ? Ok(mapper.Map<ListingDto>(result)) : NotFound();
    }

    [HttpGet("categories")]
    public async ValueTask<IActionResult> GetListingCategories(
       [FromServices] IListingCategoryService listingCategoryService,
       CancellationToken cancellationToken = default)
    {
        var result = await listingCategoryService.Get(new ListingCategoryFilter(),false).ToListAsync(cancellationToken);

        return result.Any() ? Ok(mapper.Map<List<ListingCategoryDto>>(result)) : NoContent();
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