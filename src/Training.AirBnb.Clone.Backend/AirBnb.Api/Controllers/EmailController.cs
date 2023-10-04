using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEntityBaseService<Email> _emailService;

        public EmailController(IEntityBaseService<Email> emailService)
        {
            _emailService = emailService;            
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
