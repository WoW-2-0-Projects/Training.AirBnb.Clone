using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination, CancellationToken cancellationToken)
    {
        var specification = new QuerySpecification<User>(filterPagination.PageSize, filterPagination.PageToken, true);
        var result = await userService.GetAsync(filterPagination.ToQueryPagination(true).ToQuerySpecification(), cancellationToken);

        return result.Any() ? Ok(result) : NotFound();
    }
    
    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetUserById(
        [FromRoute] Guid userId, 
        CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(userId, true, cancellationToken);
        return user is not null ? Ok(mapper.Map<UserDto>(user)) : NotFound();
    }
    
    [HttpPost]
    public async ValueTask<IActionResult> Create(
        [FromBody] UserDto userDto, 
        CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(userDto);
        var result = await userService.CreateAsync(user, cancellationToken: cancellationToken);

        return Ok(mapper.Map<UserDto>(result));
    }

    [HttpPut]
    public async ValueTask<IActionResult> Update(
        [FromBody] UserDto userDto,
        CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(userDto);
        var result =await userService.UpdateAsync(user, cancellationToken: cancellationToken);

        return Ok(mapper.Map<UserDto>(result));
    }
    
    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteByIdAsync(
        [FromRoute] Guid userId, 
        CancellationToken cancellationToken)
    {
        var user = await userService.DeleteByIdAsync(userId, cancellationToken: cancellationToken);
        return user is not null ? Ok(mapper.Map<UserDto>(user)) : NotFound();
    }
}