using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.Listings.Services;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryDetailsController : ControllerBase
{
    private readonly IListingCategoryDetailsService _listingCategoryDetailsService;
    private readonly IListingCategoryService _listingCategoryService;
    private readonly IListingTypeService _listingTypeService;

    public CategoryDetailsController(IListingCategoryDetailsService listingCategoryDetailsService,
        IListingCategoryService listingCategoryService, 
        IListingTypeService listingTypeService)
    {
        _listingCategoryDetailsService = listingCategoryDetailsService;
        _listingCategoryService = listingCategoryService;
        _listingTypeService = listingTypeService;
    }

    #region Listing Categories

    [HttpGet("categories")]
    public IActionResult GetAllCategories()
    {
        var result = _listingCategoryService.Get(category => true);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("categories/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetCategoryById(Guid categoryId)
        => Ok(await _listingCategoryService.GetByIdAsync(categoryId));
    
    [HttpPost("categories")]
    public async ValueTask<IActionResult> AddCategory([FromBody] ListingCategory category)
        => Ok(await _listingCategoryService.CreateAsync(category));

    [HttpPut("categories")]
    public async ValueTask<IActionResult> UpdateCategory([FromBody] ListingCategory category)
    {
        await _listingCategoryService.UpdateAsync(category);
        return NoContent();
    }

    [HttpDelete("categories/{categoryId:guid}")]
    public async ValueTask<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
    {
        await _listingCategoryDetailsService.DeleteCategoryAsync(categoryId);
        return NoContent();
    }

    #endregion

    #region Listing Types

    [HttpGet("listingTypes")]
    public IActionResult GetListingTypes()
    {
        var result = _listingTypeService.Get(type => true);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("listingTypes/{typeId:guid}")]
    public async ValueTask<IActionResult> GetFeatureOptionById([FromRoute] Guid typeId)
      => Ok(await _listingTypeService.GetByIdAsync(typeId));

    [HttpGet("listingTypesByCategory/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetListingTypesByCategoryId([FromRoute] Guid categoryId)
    {
        var result = await _listingCategoryDetailsService.GetListingTypesByCategoryIdAsync(categoryId);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("listingTypes")]
    public async ValueTask<IActionResult> AddListingType([FromBody] ListingType type)
        => Ok(await _listingTypeService.CreateAsync(type));

    [HttpPut("listingTypes")]
    public async ValueTask<IActionResult> UpdateListingType([FromBody] ListingType type)
    {
        await _listingTypeService.UpdateAsync(type);
        return NoContent();
    }

    [HttpDelete("listingTypes/{typeId:guid}")]
    public async ValueTask<IActionResult> DeleteListingType([FromRoute] Guid typeId)
    {
        await _listingCategoryDetailsService.DeleteListingTypeAsync(typeId);
        return NoContent();
    }

    #endregion

    #region Listing Features

    [HttpGet("listingFeatures/{listingTypeId:guid}")]
    public IActionResult GetFeaturesByTypeId(Guid listingTypeId)
    {
        var result = _listingCategoryDetailsService.GetListingFeaturesByTypeId(listingTypeId);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("listingFeatures")]
    public async ValueTask<IActionResult> AddListingFeature([FromBody] ListingFeature feature)
        => Ok(await _listingCategoryDetailsService.AddListingFeatureAsync(feature));

    [HttpPut("listingFeatures")]
    public async ValueTask<IActionResult> UpdateListingFeature([FromBody] ListingFeature feature)
    {
        await _listingCategoryDetailsService.UpdateListingFeatureAsync(feature);
        return NoContent();
    }

    [HttpDelete("listingFeatures/{featureId:guid}")]
    public async ValueTask<IActionResult> DeleteListingFeature([FromRoute] Guid featureId)
    {
        await _listingCategoryDetailsService.DeleteListingFeatureAsync(featureId);
        return NoContent();
    }

    #endregion

    #region Listing Category Types

    [HttpPost("categoryTypes")]
    public async ValueTask<IActionResult> AddCategoryType([FromBody] ListingCategoryType listingType)
        => Ok(await _listingCategoryDetailsService.AddListingCategoryTypeAsync(listingType));

    [HttpPost("categoryTypes/{categoryId:guid}/listingTypes")]
    public async ValueTask<IActionResult> AddCategoryFeatureOptions([FromRoute] Guid categoryId, [FromBody] List<Guid> listingTypes)
        => Ok(await _listingCategoryDetailsService.AddListingCategoryTypesAsync(categoryId, listingTypes));


    [HttpPut("categoryTypes/{categoryId:guid}/listingTypes")]
    public async ValueTask<IActionResult> UpdateFeatureOptionByCategoryId([FromRoute] Guid categoryId, [FromBody] List<Guid> updatedListingTypes)
        => Ok(await _listingCategoryDetailsService.UpdateListingCategoryTypesAsync(categoryId, updatedListingTypes));

    #endregion
}