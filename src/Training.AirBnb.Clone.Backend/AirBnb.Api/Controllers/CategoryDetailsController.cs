using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryDetailsController : ControllerBase
{
    private readonly IEntityBaseService<ListingCategory> _listingCategoryService;

    public CategoryDetailsController(IEntityBaseService<ListingCategory> listingCategoryService)
    {
        _listingCategoryService = listingCategoryService;
    }

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
        => Ok(await _listingCategoryService.UpdateAsync(category));

    [HttpDelete("categories/{categoryId:guid}")]
    public async ValueTask<IActionResult> DeleteCategory([FromRoute] Guid categoryId, [FromServices] IListingCategoryDetailsService listingCategoryDetailsService)
        => Ok(await listingCategoryDetailsService.DeleteCategoryAsync(categoryId));
}