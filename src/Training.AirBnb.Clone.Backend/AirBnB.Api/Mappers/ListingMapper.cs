using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AirBnB.Infrastructure.StorageFiles.Mappers;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// Mapper profile for mapping between ListingDto and Listing.
/// </summary>
public class ListingMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the ListingMapper class.
    /// </summary>
    public ListingMapper()
    {
        CreateMap<Listing, ListingDto>()
            .ForMember(dest => dest.ImagesUrls,
                opt =>
                    opt.ConvertUsing<StorageFileToUrlConverter, List<ListingMediaFile>>(src => src.ImagesStorageFile));

        CreateMap<ListingDto, Listing>();
    }
}