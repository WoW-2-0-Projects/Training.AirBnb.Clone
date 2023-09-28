using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IEntityBaseService<User> _userService;

    public UsersController(IEntityBaseService<User> userService)
    {
        _userService = userService;
    }

    [HttpPost("user")]
    public async Task<IActionResult> PostAsync(User user)
    {
        return Ok(await _userService.CreateAsync(user));
    }

    [HttpGet("user/id")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(await _userService.GetByIdAsync(id));
    }

    [HttpPut("user/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, User user)
    {
        return Ok(await _userService.UpdateAsync(user));
    }
    [HttpDelete("user/id")]
    public async Task<IActionResult> DeleteAsync(Guid id) => Ok(await _userService.DeleteAsync(id));

    [HttpDelete("user/user")]
    public async Task<IActionResult> DeleteAsync(User user) => Ok(await _userService.DeleteAsync(user));
}