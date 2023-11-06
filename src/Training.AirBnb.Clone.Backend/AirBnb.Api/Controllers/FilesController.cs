using AutoMapper;
using Backend_Project.Application.Files.Dtos;
using Backend_Project.Application.Files.Services;
using Backend_Project.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IFileProcessingService _fileProcessingService;

    public FilesController(IMapper mapper, IFileProcessingService fileProcessingService)
    {
        _mapper = mapper;
        _fileProcessingService = fileProcessingService;
    }

    [HttpPost("listingImages/uploadImage")]
    public async ValueTask<IActionResult> UploadListingImageAsync(IFormFile file, Guid listingId)
    {
        var uploadFile = _mapper.Map<UploadFileDto>(file);
        uploadFile.ListingId = listingId;
        uploadFile.UserId = Guid.Empty;
        uploadFile.Type = ImageType.Listing;

        return Ok(await _fileProcessingService.UploadImageAsync(uploadFile));
    }

    [HttpPost("profilePictures/uploadImage")]
    public async ValueTask<IActionResult> UploadProfilePictureAsync(IFormFile file)
    {
        var uploadFile = _mapper.Map<UploadFileDto>(file);
        uploadFile.UserId = Guid.Empty;
        uploadFile.Type = ImageType.User;

        return Ok(await _fileProcessingService.UploadImageAsync(uploadFile));
    }

    [HttpDelete("listingImages/{imageId}")]
    public async ValueTask<IActionResult> DeleteListingImage(Guid imageId)
        => Ok(await _fileProcessingService.DeleteListingImageAsync(imageId));
}