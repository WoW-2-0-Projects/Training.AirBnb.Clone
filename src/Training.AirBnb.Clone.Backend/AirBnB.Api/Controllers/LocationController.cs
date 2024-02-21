using AirBnB.Application.Currencies.Services;
using AirBnB.Application.Locations.Models;
using AirBnB.Application.Locations.Services;
using AirBnB.Domain.Common.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LocationController(
    ICountryService countryService, 
    ICityService cityService,
    ICurrencyService currencyService) : ControllerBase
{
    [HttpGet("countries")]
    public async ValueTask<IActionResult> Get(
        [FromQuery] FilterPagination filterPagination,
        CancellationToken cancellationToken = default)
    {
        var result = await countryService.Get(filterPagination, true).ToListAsync(cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("cities")]
    public async ValueTask<IActionResult> GetCities(
        [FromQuery]FilterPagination filterPagination,
        CancellationToken cancellationToken = default)
    {
        var result = await cityService.Get(filterPagination, true).ToListAsync(cancellationToken);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("currencies")]
    public async ValueTask<IActionResult> GetCurrencies(
        [FromQuery] FilterPagination filterPagination,
        CancellationToken cancelltionToken = default)
    {
        var result = await currencyService.Get(filterPagination, false).ToListAsync(cancelltionToken);
        return result.Any() ? Ok(result) : NoContent();
    }
}