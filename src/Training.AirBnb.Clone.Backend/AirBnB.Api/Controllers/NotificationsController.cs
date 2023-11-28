using AirBnB.Application.Common.Models.Querying;
using AirBnB.Application.Common.Notifications.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(IEmailTemplateService emailTemplateService,
    ISmsTemplateService smsTemplateService) : ControllerBase
{
    [HttpGet("templates/sms")]
    public async ValueTask<IActionResult> GetSmsTemplates([FromQuery] FilterPagination pagination) =>
        Ok(await smsTemplateService.GetByFilterAsync(pagination));

    [HttpGet("templates/email")]
    public async ValueTask<IActionResult> GetEmailTemplates([FromQuery] FilterPagination pagination) =>
        Ok(await emailTemplateService.GetByFilterAsync(pagination));
}