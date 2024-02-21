using AirBnB.Api.Models.DTOs;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Domain.Entities;
using AirBnB.Infrastructure.StorageFiles.Mappers;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class ListingMediaFileMapper : Profile
{
    public ListingMediaFileMapper()
    {
        CreateMap<UploadFileInfoDto, ListingMediaFile>()
            .ForMember(dest => dest.ListingId, opt => opt.MapFrom(src => src.OwnerId));

        CreateMap<ListingMediaFile, ListingMediaFileDto>()
            .ForMember(dest => dest.ImageUrl,
                opt => opt.ConvertUsing<StorageFileToUrlConverter, StorageFile>(src => src.StorageFile))
            .ReverseMap();
    }
}