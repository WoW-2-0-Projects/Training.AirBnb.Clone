using Backend_Project.Application.Listings.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
public class ListingsRegistrationController : ControllerBase
{
    private readonly IListingRegistrationService _listingsManagementService;

    public ListingsRegistrationController(IListingRegistrationService listingManagementService)
    {
        _listingsManagementService = listingManagementService;
    }

    [HttpPost("registrationListing/{title}")]
    public async ValueTask<IActionResult> CreateListing([FromRoute] string title)
        => Ok(await _listingsManagementService.CreateListing(title));
}