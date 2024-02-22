using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Listings.Models;
using AirBnB.Application.Listings.Services;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Application.StorageFiles.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController(
    IListingService listingService, 
    IListingMediaFileService listingMediaFileService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination)
    {
        var result = listingService.Get(filterPagination, true);
        
        return new ValueTask<IActionResult>(Ok(mapper.Map<List<ListingDto>>(result)));
    }

    [HttpGet("category/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetListingByCategoryId([FromQuery] FilterPagination filterPagination,
        [FromRoute] Guid categoryId, CancellationToken cancellationToken = default)
    {
        var listings = await listingService.GetByCategoryId(filterPagination, categoryId, true).ToListAsync(cancellationToken);
        return listings.Count != 0 ? Ok(mapper.Map<List<ListingDto>>(listings)) : NoContent();
    }

    [HttpGet("listingImages/{listingId:guid}")]
    public IActionResult GetListingImagesByListingId([FromRoute] Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var result = listingMediaFileService.GetListingMediaFilesByListingId(listingId);

        return Ok(mapper.Map<List<ListingMediaFileDto>>(result));
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
        CancellationToken cancellationToken = default)
    {
        var listing = mapper.Map<Listing>(listingDto);
        var result = await listingService.CreateAsync(listing, true, cancellationToken);

        return Ok(mapper.Map<ListingDto>(result));
    }

    [HttpPost("listingImages/{listingId:guid}")]
    public async ValueTask<IActionResult> UploadListingImage(
        [FromForm] IFormFile listingImage,
        [FromRoute] Guid listingId,
        CancellationToken cancellationToken = default)
    {
        var listingFileInfo = mapper.Map<UploadFileInfoDto>(listingImage);
        listingFileInfo.OwnerId = listingId;
        listingFileInfo.StorageFileType = StorageFileType.ListingImage;

        return Ok(await listingMediaFileService
            .CreateAsync(listingFileInfo, cancellationToken: cancellationToken));
    }

    [HttpPut("listingImages")]
    public async ValueTask<IActionResult> UpdateImageOrder(IEnumerable<ListingMediaFileDto> listingMediaFileDtos,
        CancellationToken cancellationToken = default)
    {
        var mediaFiles = mapper.Map<List<ListingMediaFile>>(listingMediaFileDtos);

        await listingMediaFileService
            .ReorderListingMediaFiles(mediaFiles, cancellationToken: cancellationToken);
        
        return NoContent();
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

    [HttpDelete("listingImages/{listingMediaFileId:guid}")]
    public async ValueTask<IActionResult> DeleteListingImageById([FromRoute] Guid listingMediaFileId,
        CancellationToken cancellationToken = default)
    {
        await listingMediaFileService
            .DeleteByIdAsync(listingMediaFileId, cancellationToken: cancellationToken);

        return NoContent();
    }
}