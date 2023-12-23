using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Query;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IEmailTemplateService emailTemplateService, ISmsTemplateService smsTemplateService, IMapper mapper) : ControllerBase
{
    [HttpGet("templates/sms")]
    public async ValueTask<IActionResult> GetSmsTemplates([FromQuery] FilterPagination filterPagination,
        CancellationToken cancellationToken)
    {
        var result = await smsTemplateService.GetAsync(filterPagination.ToQueryPagination(true).ToQuerySpecification(),
            cancellationToken);

        return result.Any() ? Ok(mapper.Map<SmsTemplateDto>(result)) : NotFound();
    }
    
    [HttpGet("templates/email")]
    public async ValueTask<IActionResult> GetEmailTemplates([FromQuery] FilterPagination filterPagination,
        CancellationToken cancellationToken)
    {
        var result = await emailTemplateService.GetAsync(filterPagination.ToQueryPagination(true).ToQuerySpecification(),
            cancellationToken);

        return result.Any() ? Ok(mapper.Map<SmsTemplateDto>(result)) : NotFound();
    }
}