using AutoMapper;
using Backend_Project.Application.Foundations.NotificationServices;
using Backend_Project.Application.Notifications.Dtos;
using Backend_Project.Application.Notifications.Services;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IEmailManagementService _emailManagementService;
        private readonly IMapper _mapper;

        public NotificationsController(
            IEmailTemplateService entityBaseService, 
            IEmailService emailService, 
            IEmailManagementService emailManagementService,
            IMapper mapper)
        {
            _emailTemplateService = entityBaseService;
            _emailService = emailService;
            _emailManagementService = emailManagementService;
            _mapper = mapper;
        }
        
        #region SendEmail
        [HttpPost("emailManagement{userId:guid}/{templateId:guid}")]
        public async Task<IActionResult> SendEmail(Guid userId, Guid templateId)
            => Ok(await _emailManagementService.SendEmailAsync(userId, templateId));
        #endregion
        
        #region GetAllTemplates
        [HttpGet("emailTemplates")]
        public IActionResult GetAllTemplates()
        {
            var result = _emailTemplateService.Get(emailTemplate => true)
                .Select(et => _mapper.Map<EmailTemplateDto>(et)).ToList();
            return result.Any() ? Ok(result) : NoContent();
        }
        #endregion
        
        #region GetByTemplateId
        [HttpGet("emailTemlates/emailTemplateId:guid")]
        public async Task<IActionResult> GetByTemplateId(Guid templateId, CancellationToken cancellationToken)
            => Ok(_mapper.Map<EmailTemplateDto>(await _emailTemplateService.GetByIdAsync(templateId, cancellationToken)));
        #endregion
        
        #region AddTemplate
        [HttpPost("emailTemplates")]
        public async Task<IActionResult> AddTemplate([FromBody] EmailTemplateDto emailTemplate, bool saveChance, CancellationToken cancellationToken)
           =>  Ok(_mapper.Map<EmailTemplateDto>(await _emailTemplateService.CreateAsync(_mapper.Map<EmailTemplate>(emailTemplate), saveChance, cancellationToken)));
        #endregion
        
        #region UpdataTemplate
        [HttpPut("emailTemplates")]
        public async Task<IActionResult> UpdateTemplate([FromBody] EmailTemplateDto emailTemplate, bool saveChance, CancellationToken cancellationToken)
        {
            Ok(_mapper.Map<EmailTemplateDto>(await _emailTemplateService.UpdateAsync(_mapper.Map<EmailTemplate>(emailTemplate), saveChance, cancellationToken)));
            return NoContent();
        }
        #endregion
        
        #region DeleteTemplate
        [HttpDelete("emailTemplate/{emailTemplateId:guid}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] Guid emailTemplateId)
        {
            Ok(await _emailTemplateService.DeleteAsync(emailTemplateId));
            return NoContent();
        }
        #endregion

        #region Emails
        [HttpGet("emails")]
        public IActionResult GetAllEmails()
        {
            var result = _emailService.Get(email => true)
                .Select(e=>_mapper.Map<EmailDto>(e)).ToList();
            
            return result.Any() ? Ok(result) : NoContent();
        }
        #endregion
        
        #region GetEmailById
        [HttpGet("emails/{emailId:guid}")]
        public async ValueTask<IActionResult> GetEmailById(Guid emailId, CancellationToken cancellationToken)
            => Ok(_mapper.Map<EmailDto>(await _emailService.GetByIdAsync(emailId, cancellationToken)));
        #endregion

        #region  AddEmail
        [HttpPost("emails")]
        public async ValueTask<IActionResult> AddEmails([FromBody] EmailDto email, bool saveChange, CancellationToken cancellationToken)
            => Ok(_mapper.Map<EmailDto>(await _emailService.CreateAsync(_mapper.Map<Email>(email), saveChange, cancellationToken)));        
        #endregion
    }
}