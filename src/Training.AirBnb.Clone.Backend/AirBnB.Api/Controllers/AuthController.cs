using System.Security.Authentication;
using System.Security.Claims;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
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
    
    [Authorize(Roles = "Admin")]
    [HttpPost("users/{userId:guid}/roles/{roleType}")]
    public async Task<IActionResult> GrandRole([FromRoute] Guid userId, [FromRoute] string roleType, CancellationToken cancellationToken = default)
    {
        var actionUserId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimConstants.UserId)).Value);
        if (actionUserId == default) throw new AuthenticationException("does not set object");
        var result = await authService.GrandRoleAsync(userId, roleType, actionUserId, cancellationToken);
        return result ? Ok(result) : NoContent();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("users/{userId:guid}/roles/{roleType}")]
    public async Task<IActionResult> RevokeRole([FromRoute] Guid userId, [FromRoute] string roleType, CancellationToken cancellationToken = default)
    {
        var actionUserId = Guid.Parse(User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimConstants.UserId)).Value);
        var actionUserRole = Enum.Parse<RoleType>(User.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)).Value);
        
        var result = await authService.RevokeRoleAsync(userId, roleType, actionUserId, actionUserRole, cancellationToken);
        return result ? Ok(result) : NoContent();
    }
} 