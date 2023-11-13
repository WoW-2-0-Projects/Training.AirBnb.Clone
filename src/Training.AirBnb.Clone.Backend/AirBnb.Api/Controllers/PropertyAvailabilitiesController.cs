using AutoMapper;
using Backend_Project.Application.Availabilities.Dtos;
using Backend_Project.Application.Availabilities.Services;
using Backend_Project.Application.Foundations.ListingServices;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyAvailabilitiesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAvailabilityProcessingService _availabilityProcessingService;
    private readonly IAvailabilityService _availabilityService;
    private readonly IListingRulesService _listingRulesService;
    private readonly IBlockedNightService _blockedNightService;

    public PropertyAvailabilitiesController(
        IMapper mapper,
        IAvailabilityProcessingService availabilityProcessingService, 
        IAvailabilityService availabilityService,
        IListingRulesService listingRulesService,
        IBlockedNightService blockedNightService
    )
    {
        _mapper = mapper;
        _availabilityProcessingService = availabilityProcessingService;
        _availabilityService = availabilityService;
        _listingRulesService = listingRulesService;
        _blockedNightService = blockedNightService;
    }

    [HttpPost("rules")]
    public async ValueTask<IActionResult> CreateListingRuleAsync(
        [FromBody] ListingRulesRegistrationDto rule, 
        CancellationToken cancellationToken
    )
    {
        var result = await _listingRulesService
           .CreateAsync(_mapper.Map<ListingRules>(rule), cancellationToken: cancellationToken);

        return Ok(_mapper.Map<ListingRulesRegistrationDto>(result));
    }

    [HttpPut("rules")]
    public async ValueTask<IActionResult> UpdateListingRuleAsync([FromBody] ListingRulesDto rule, CancellationToken cancellationToken)
    {
        var result = await _listingRulesService
            .UpdateAsync(_mapper.Map<ListingRules>(rule), cancellationToken: cancellationToken);

        return Ok(_mapper.Map<ListingRulesDto>(result));
    }

    [HttpPut("availabilities")]
    public async ValueTask<IActionResult> CreateAvailabilityAsync([FromBody] AvailabilityDto availability, CancellationToken cancellationToken)
    {
        var result = await _availabilityService
            .CreateAsync(_mapper.Map<Availability>(availability), cancellationToken: cancellationToken);

        return Ok(_mapper.Map<AvailabilityDto>(result));
    }

    [HttpGet("blockedNights/{listingId}")]
    public IActionResult GetBlockedNightsByListingId([FromRoute] Guid listingId)
    {
        var result = _availabilityProcessingService
            .GetBlockedNightsByListingId(listingId)
            .Select(_mapper.Map<BlockedNightDto>);

        return Ok(result);
    }

    [HttpPost("blockedNights")]
    public async ValueTask<IActionResult> CreateBlockedNightAsync([FromBody] BlockedNightDto night, CancellationToken cancellationToken)
    {
        var blockedNight = _mapper.Map<BlockedNight>(night);
        blockedNight.IsCustomBlock = true;

        var result = await _availabilityProcessingService
            .CreateBlockedNightAsync(blockedNight, cancellationToken: cancellationToken);

        return Ok(_mapper.Map<BlockedNightDto>(result));
    }

    [HttpDelete("blockedNights/{blockedNightId}")]
    public async ValueTask<IActionResult> DeleteBlockedNightAsync([FromRoute] Guid blockedNightId, CancellationToken cancellationToken)
    {
        await _blockedNightService.DeleteAsync(blockedNightId, cancellationToken: cancellationToken);

        return NoContent();
    }
}