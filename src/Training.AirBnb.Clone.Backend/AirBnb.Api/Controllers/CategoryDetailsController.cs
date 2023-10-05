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
    private readonly IEntityBaseService<ListingFeatureOption> _listingFeatureOptionService;

    public CategoryDetailsController(IListingCategoryDetailsService listingCategoryDetailsService, IEntityBaseService<ListingCategory> listingCategoryService, IEntityBaseService<ListingFeatureOption> listingFeatureOptionService)
    {
        _listingCategoryDetailsService = listingCategoryDetailsService;
        _listingCategoryService = listingCategoryService;
        _listingFeatureOptionService = listingFeatureOptionService;
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

    #region Listing Feature Options

    [HttpGet("featureOptions")]
    public IActionResult GetFeatureOptions()
    {
        var result = _listingFeatureOptionService.Get(option => true);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("featureOptions/{featureOptionId:guid}")]
    public async ValueTask<IActionResult> GetFeatureOptionById([FromRoute] Guid featureOptionId)
      => Ok(await _listingFeatureOptionService.GetByIdAsync(featureOptionId));

    [HttpGet("featureOptionsByCategory/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetFeatureOptionsByCategoryId([FromRoute] Guid categoryId)
    {
        var result = await _listingCategoryDetailsService.GetFeatureOptionsByCategoryIdAsync(categoryId);
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("featureOptions")]
    public async ValueTask<IActionResult> AddFeatureOption([FromBody] ListingFeatureOption featureOption)
        => Ok(await _listingFeatureOptionService.CreateAsync(featureOption));

    [HttpPut("featureOptions")]
    public async ValueTask<IActionResult> UpdateFeatureOption([FromBody] ListingFeatureOption featureOption)
    {
        await _listingFeatureOptionService.UpdateAsync(featureOption);
        return NoContent();
    }

    [HttpDelete("featureOptions/{featureOptionId:guid}")]
    public async ValueTask<IActionResult> DeleteFeatureOption([FromRoute] Guid featureOptionId)
    {
        await _listingCategoryDetailsService.DeleteFeatureOptionAsync(featureOptionId);
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