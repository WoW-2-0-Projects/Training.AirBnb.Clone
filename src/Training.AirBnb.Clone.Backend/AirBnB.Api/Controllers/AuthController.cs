using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService, IMapper mapper) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await authService.SignUpAsync(signUpDetails, cancellationToken);
        return result ? Ok(result) : BadRequest();
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInDetails signInDetails, CancellationToken cancellationToken)
    {
        var result = await authService.SignInAsync(signInDetails, cancellationToken);
        return Ok(result);
    }
}