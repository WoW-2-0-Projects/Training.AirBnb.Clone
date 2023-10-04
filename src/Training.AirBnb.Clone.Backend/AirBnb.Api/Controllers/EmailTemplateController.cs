using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEntityBaseService<EmailTemplate> _emailTemplateService;
        public EmailTemplateController(IEntityBaseService<EmailTemplate> entityBaseService)
        {
            _emailTemplateService = entityBaseService;
        }
        
        [HttpPost("emailTemplate")]
        public async Task<IActionResult> TemplateAsync(EmailTemplate emailTemplate)
        {
            return Ok(await _emailTemplateService.CreateAsync(emailTemplate));
        }
        
        [HttpGet("emailTemlate/id")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await _emailTemplateService.GetByIdAsync(id));
        }
        
        [HttpPut("emailTemplate/{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, EmailTemplate emailTemplate)
        {
            return Ok(await _emailTemplateService.UpdateAsync(emailTemplate));
        }
        
        [HttpDelete("emailTemplate/id")]
        public async Task<IActionResult> DeleteAsync(Guid id) => Ok(await _emailTemplateService.DeleteAsync(id));
        
        [HttpDelete("emailTemplate/emailTemplate")]
        public async Task<IActionResult> DeleteAsync(EmailTemplate emailTemplate) => Ok(await _emailTemplateService.DeleteAsync(emailTemplate));

    }
}
