using System.Security.Authentication;
using System.Security.Claims;
using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMapper mapper, IAuthService authService, IRequestUserContextProvider requestUserContextProvider) : ControllerBase
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
        return Ok(mapper.Map<IdentityTokenDto>(result));
    }
    
    
    [Authorize(Roles = "Admin, System")]
    [HttpPost("users/{userId:guid}/roles/{roleType}")]
    public async Task<IActionResult> GrandRole([FromRoute] Guid userId, [FromRoute] string roleType, CancellationToken cancellationToken = default)
    {
        var result = await authService.GrandRoleAsync(userId, roleType, cancellationToken);
        return result ? Ok(result) : NoContent();
    }
    
    [Authorize(Roles = "Admin, System")]
    [HttpDelete("users/{userId:guid}/roles/{roleType}")]
    public async Task<IActionResult> RevokeRole([FromRoute] Guid userId, [FromRoute] string roleType, CancellationToken cancellationToken = default)
    {
        var result = await authService.RevokeRoleAsync(userId, roleType, cancellationToken);
        return result ? Ok(result) : NoContent();
    }
} 