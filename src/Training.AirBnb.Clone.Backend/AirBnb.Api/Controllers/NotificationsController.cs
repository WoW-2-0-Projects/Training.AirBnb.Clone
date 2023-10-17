using Backend_Project.Application.Entity;
using Backend_Project.Application.Notifications;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IEntityBaseService<EmailTemplate> _emailTemplateService;
        private readonly IEntityBaseService<Email> _emailService;
        private readonly IEmailMenegmentService _emailMenagmentService;

        public NotificationsController(IEntityBaseService<EmailTemplate> entityBaseService, IEntityBaseService<Email> emailService, IEmailMenegmentService emailManagementService)
        {
            _emailTemplateService = entityBaseService;
            _emailService = emailService;
            _emailMenagmentService = emailManagementService;
        }

        [HttpPost("emailMenagment")]
        public async Task<IActionResult> SendEmail([FromRoute] Guid userId, [FromRoute] Guid templateId)
            => Ok(await _emailMenagmentService.SendEmailAsync(userId, templateId));
        
        
        [HttpGet("emailtemplates")]
        public IActionResult GetAllTemplates()
        {
            var result = _emailTemplateService.Get(emailtemplate => true).ToList();
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("emailTemlates/emailTemplateId:guid")]
        public async Task<IActionResult> GetByTemplateId(Guid id)
            => Ok(await _emailTemplateService.GetByIdAsync(id));
        
        [HttpPost("emailTemplates")]
        public async Task<IActionResult> AddTemplate([FromBody] EmailTemplate emailTemplate)
           =>  Ok(await _emailTemplateService.CreateAsync(emailTemplate));

        [HttpPut("emailTemplates")]
        public async Task<IActionResult> UpdateTemplate([FromBody] EmailTemplate emailTemplate)
        {
            Ok(await _emailTemplateService.UpdateAsync(emailTemplate));
            return NoContent();
        }

        [HttpDelete("emailTemplate/{emailTemplateId:guid}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] Guid emailTemplateId)
        {
            Ok(await _emailTemplateService.DeleteAsync(emailTemplateId));
            return NoContent();
        }


        [HttpGet("emails")]
        public IActionResult GetAllEmails()
        {
            var result = _emailService.Get(email => true).ToList();
            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpGet("emails/{emailId:guid}")]
        public async ValueTask<IActionResult> GetEmailById(Guid emailId)
            => Ok(await _emailService.GetByIdAsync(emailId));

        [HttpPost("emails")]
        public async ValueTask<IActionResult> AddEmails([FromBody] Email email)
            => Ok(await _emailService.CreateAsync(email));        
    }
}