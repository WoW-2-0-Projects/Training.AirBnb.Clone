using AutoMapper;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Application.ListingCategoryDetails.Services;
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
    private readonly IMapper _mapper;

    public CategoryDetailsController(IListingCategoryDetailsService listingCategoryDetailsService,
        IListingCategoryService listingCategoryService,
        IListingTypeService listingTypeService,
        IMapper mapper)
    {
        _listingCategoryDetailsService = listingCategoryDetailsService;
        _listingCategoryService = listingCategoryService;
        _listingTypeService = listingTypeService;
        _mapper = mapper;
    }

    #region Listing Categories

    [HttpGet("categories")]
    public IActionResult GetAllCategories()
    {
        var result = _listingCategoryService.Get(category => true)
            .Select(lc => _mapper.Map<ListingCategoryDto>(lc));
        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("categories/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetCategoryById(Guid categoryId)
        => Ok(_mapper.Map<ListingCategoryDto>(await _listingCategoryService.GetByIdAsync(categoryId)));

    [HttpPost("categories")]
    public async ValueTask<IActionResult> AddCategory([FromBody] ListingCategoryDto category)
        => Ok(_mapper.Map<ListingCategoryDto>( await _listingCategoryService.CreateAsync(_mapper.Map<ListingCategory>(category))));

    [HttpPut("categories")]
    public async ValueTask<IActionResult> UpdateCategory([FromBody] ListingCategoryDto category)
    {
        await _listingCategoryService.UpdateAsync(_mapper.Map<ListingCategory>(category));
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
        var result = _listingTypeService.Get(type => true)
            .Select(lt => _mapper.Map<ListingTypeDto>(lt));

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpGet("listingTypes/{typeId:guid}")]
    public async ValueTask<IActionResult> GetFeatureOptionById([FromRoute] Guid typeId)
      => Ok(_mapper.Map<ListingTypeDto>(await _listingTypeService.GetByIdAsync(typeId)));

    [HttpGet("listingTypesByCategory/{categoryId:guid}")]
    public async ValueTask<IActionResult> GetListingTypesByCategoryId([FromRoute] Guid categoryId)
    {
        var resultListingType = await _listingCategoryDetailsService.GetListingTypesByCategoryIdAsync(categoryId);

        var result = resultListingType.Select(lt => _mapper.Map<ListingTypeDto>(lt));

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("listingTypes")]
    public async ValueTask<IActionResult> AddListingType([FromBody] ListingTypeDto type)
        => Ok(_mapper.Map<ListingTypeDto>(await _listingTypeService.CreateAsync(_mapper.Map<ListingType>(type))));

    [HttpPut("listingTypes")]
    public async ValueTask<IActionResult> UpdateListingType([FromBody] ListingTypeDto type)
    {
        await _listingTypeService.UpdateAsync(_mapper.Map<ListingType>(type));
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
        var resultListingFeature = _listingCategoryDetailsService.GetListingFeaturesByTypeId(listingTypeId);
        var result = resultListingFeature.Select(lf => _mapper.Map<ListingFeatureDto>(lf));

        return result.Any() ? Ok(result) : NotFound();
    }

    [HttpPost("listingFeatures")]
    public async ValueTask<IActionResult> AddListingFeature([FromBody] ListingFeatureDto featureDto)
        => Ok(_mapper.Map<ListingFeatureDto>( await _listingCategoryDetailsService.AddListingFeatureAsync(_mapper.Map<ListingFeature>(featureDto))));

    [HttpPut("listingFeatures")]
    public async ValueTask<IActionResult> UpdateListingFeature([FromBody] ListingFeatureDto featureDto)
    {
        await _listingCategoryDetailsService.UpdateListingFeatureAsync(_mapper.Map<ListingFeature>(featureDto));
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
    public async ValueTask<IActionResult> AddCategoryType([FromBody] ListingCategoryTypeDto listingTypeDto)
        => Ok(_mapper.Map<ListingCategoryTypeDto>(await _listingCategoryDetailsService.AddListingCategoryTypeAsync(_mapper.Map<ListingCategoryType>(listingTypeDto))));

    [HttpPost("categoryTypes/{categoryId:guid}/listingTypes")]
    public async ValueTask<IActionResult> AddCategoryFeatureOptions([FromRoute] Guid categoryId, [FromBody] List<Guid> listingTypes)
        => Ok(await _listingCategoryDetailsService.AddListingCategoryTypesAsync(categoryId, listingTypes));


    [HttpPut("categoryTypes/{categoryId:guid}/listingTypes")]
    public async ValueTask<IActionResult> UpdateFeatureOptionByCategoryId([FromRoute] Guid categoryId, [FromBody] List<Guid> updatedListingTypes)
        => Ok(await _listingCategoryDetailsService.UpdateListingCategoryTypesAsync(categoryId, updatedListingTypes));

    #endregion
}