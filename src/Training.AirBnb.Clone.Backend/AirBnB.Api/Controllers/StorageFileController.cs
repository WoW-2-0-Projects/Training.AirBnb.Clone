using AirBnB.Api.Models.DTOs;
using AirBnB.Application.Common.StorageFiles;
using AirBnB.Domain.Common.Query;
using AirBnB.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirBnB.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StorageFileController(IStorageFileService storageFileService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] FilterPagination filterPagination, CancellationToken cancellationToken)
    {
        var storageFiles = await storageFileService.GetAsync(filterPagination.ToQueryPagination(true).ToQuerySpecification(), cancellationToken);
        var result = new List<StorageFileDto>();
        
        foreach (var file in storageFiles)
            result.Add(mapper.Map<StorageFileDto>(file));
        
        return result.Any() ? Ok(result) : NotFound();
    }
}