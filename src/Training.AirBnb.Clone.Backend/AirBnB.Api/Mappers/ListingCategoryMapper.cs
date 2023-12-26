using AirBnB.Api.Models.DTOs.Listings;
using AirBnB.Domain.Entities.Listings;
using AirBnB.Domain.Entities.StorageFiles;
using AirBnB.Infrastructure.StorageFiles.Mappers;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between the User and UserDto classes.
/// </summary>
public class ListingCategoryMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the ListingCategoryMapper
    /// </summary>
    public ListingCategoryMapper()
    {
        CreateMap<ListingCategory, ListingCategoryDto>().
            ForMember(dest => dest.ImageUrl, opt => opt.ConvertUsing<StorageFileToUrlConverter, StorageFile>(src => src.ImageStorageFile));
    }
}