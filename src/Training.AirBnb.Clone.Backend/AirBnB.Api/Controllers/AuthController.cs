using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDetails registrationDetails, CancellationToken cancellationToken)
    {
        var result = await authService.RegisterAsync(registrationDetails, cancellationToken);
        return result ? Ok(result) : BadRequest();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDetails loginDetails, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(loginDetails);
        return Ok(result);
    }
}