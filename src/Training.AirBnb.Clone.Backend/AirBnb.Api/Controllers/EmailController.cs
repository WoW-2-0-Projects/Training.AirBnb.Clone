using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace AirBnb.Api.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class EmailController : Controller
    {
        private readonly IEntityBaseService<Email> _emailService;

        public EmailController(IEntityBaseService<Email> emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("email")]
        public async Task<IActionResult> EmailAsync(Email email)
        {
            return Ok(await _emailService.CreateAsync(email));
        }

        [HttpGet("email/id")]
        public async Task<IActionResult> GetByAsync(Guid id)
        {
            return Ok(await _emailService.GetByIdAsync(id));
        }
    }
}
