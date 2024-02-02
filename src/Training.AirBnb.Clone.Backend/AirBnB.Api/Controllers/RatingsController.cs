using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Ratings.Services;
using AirBnB.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingsController(IMapper mapper, IGuestFeedbackService guestFeedbackService) : ControllerBase
{
    [HttpGet("{listingId:guid}")]
    public async ValueTask<IActionResult> GetFeedbacksByListingId([FromRoute]Guid listingId, CancellationToken cancellationToken = default)
    {
        return Ok(mapper.Map<IEnumerable<GuestFeedbackDto>>(await guestFeedbackService.GetByListingIdAsync(listingId)));
    }
    
    [HttpPost()]
    public async ValueTask<IActionResult> CreateFeedbackAsync([FromBody]GuestFeedbackDto guestFeedback,
        CancellationToken cancellationToken = default)
    {
        return Ok(await guestFeedbackService
            .CreateAsync(mapper.Map<GuestFeedback>(guestFeedback), true, cancellationToken));
    }

    [HttpDelete("{feedbackId:guid}")]
    public async ValueTask<IActionResult> DeleteFeedbackByIdAsync([FromRoute] Guid feedbackId, 
        CancellationToken cancellationToken = default)
    {
        return Ok(await guestFeedbackService.DeleteByIdAsync(feedbackId, cancellationToken: cancellationToken));
    }
}