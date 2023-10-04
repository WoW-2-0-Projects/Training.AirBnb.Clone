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

        [HttpGet("emailtemplates")]
        public IActionResult GetAllTemplates()
        {
            var result = _emailTemplateService.Get(emailtemplate => true).ToList();
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("emailTemlates/emailTemplateId:guid")]
        public async Task<IActionResult> GetByTemplateIdAsync(Guid id)
            => Ok(await _emailTemplateService.GetByIdAsync(id));
        
        [HttpPost("emailTemplates")]
        public async Task<IActionResult> AddTemplateAsync([FromBody] EmailTemplate emailTemplate)
           =>  Ok(await _emailTemplateService.CreateAsync(emailTemplate));

        [HttpPut("emailTemplates")]
        public async Task<IActionResult> UpdateAsync([FromBody] EmailTemplate emailTemplate)
        {
            Ok(await _emailTemplateService.UpdateAsync(emailTemplate));
            return NoContent();
        }

        [HttpDelete("emailTemplate/{emailTemplateId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid emailTemplateId)
        {
            Ok(await _emailTemplateService.DeleteAsync(emailTemplateId));
            return NoContent();
        }

    }
}
