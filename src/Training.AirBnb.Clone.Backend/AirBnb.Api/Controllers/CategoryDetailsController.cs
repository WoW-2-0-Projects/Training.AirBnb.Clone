using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryDetailsController : ControllerBase
{
    private readonly IListingCategoryDetailsService _listingCategoryDetailsService;
    private readonly IEntityBaseService<ListingCategory> _listingCategoryService;
    private readonly IEntityBaseService<ListingType> _listingTypeService;

    public CategoryDetailsController(IListingCategoryDetailsService listingCategoryDetailsService, IEntityBaseService<ListingCategory> listingCategoryService, IEntityBaseService<ListingType> listingTypeService)
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

    [HttpGet("listingFeatures/{featureOptionId:guid}")]
    public IActionResult GetFeaturesByOptionId(Guid featureOptionId)
    {
        var result = _listingCategoryDetailsService.GetListingFeaturesByOptionId(featureOptionId);
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

    #region Listing Category Feature Options

    [HttpPost("categoryFeatureOptions")]
    public async ValueTask<IActionResult> AddCategoryFeatureOption([FromBody] ListingCategoryFeatureOption featureOption)
        => Ok(await _listingCategoryDetailsService.AddListingCategoryFeatureOptionAsync(featureOption));

    [HttpPost("categoryFeatureOptions/{categoryId:guid}/featureOptions")]
    public async ValueTask<IActionResult> AddCategoryFeatureOptions([FromRoute] Guid categoryId, [FromBody] List<Guid> featureOptions)
        => Ok(await _listingCategoryDetailsService.AddListingCategoryFeatureOptionsAsync(categoryId, featureOptions));


    [HttpPut("categoryFeatureOptions/{categoryId:guid}/featureOptions")]
    public async ValueTask<IActionResult> UpdateFeatureOptionByCategoryId([FromRoute] Guid categoryId, [FromBody] List<Guid> updatedFeatureOptions)
        => Ok(await _listingCategoryDetailsService.UpdateListingCategoryFeatureOptionsAsync(categoryId, updatedFeatureOptions));

    #endregion
}