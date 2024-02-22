using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.Identity.Queries;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Application.StorageFiles.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController(IMediator mediator, IUserService userService, IMapper mapper, IRequestUserContextProvider requestUserContextProvider, IUserProfileMediaFileService userProfileMediaFileService) : ControllerBase
{
    #region Users
    
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination, CancellationToken cancellationToken)
    {
        var result =  userService.Get();
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

    [HttpGet("by-email/{emailAddress}")]
    public async ValueTask<IActionResult> CheckUserByEmail([FromRoute] string emailAddress, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CheckUserByEmailAddressQuery
            {
                EmailAddress = emailAddress
            },
            cancellationToken
        );

        return result is not null ? Ok(result) : NotFound();
    }
    
    [HttpGet("by-phone/{phoneNumber}")]
    public async ValueTask<IActionResult> CheckByPhoneNumber([FromRoute] string phoneNumber, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CheckUserByPhoneNumberQuery()
            {
                PhoneNumber = phoneNumber
            },
            cancellationToken
        );

        return result is not null ? Ok(result) : NotFound();
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

    [HttpPost("profilePictures")]
    public async ValueTask<IActionResult> UploadProfilePicture([FromForm] IFormFile profilePicture,
        CancellationToken cancellationToken = default)
    {
        var uploadFileInfo = mapper.Map<UploadFileInfoDto>(profilePicture);
        uploadFileInfo.StorageFileType = StorageFileType.UserProfileImage;
        uploadFileInfo.OwnerId = requestUserContextProvider.GetUserId();

        var result =
            await userProfileMediaFileService.CreateAsync(uploadFileInfo, cancellationToken: cancellationToken);
        
        return Ok(mapper.Map<UserProfilePictureDto>(result));
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
    
    #endregion
}