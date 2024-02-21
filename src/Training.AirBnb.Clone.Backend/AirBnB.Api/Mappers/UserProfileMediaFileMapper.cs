using AirBnB.Api.Models.DTOs;
using AirBnB.Application.StorageFiles.Models;
using AirBnB.Domain.Entities;
using AirBnB.Infrastructure.StorageFiles.Mappers;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class UserProfileMediaFileMapper : Profile
{
    public UserProfileMediaFileMapper()
    {
        CreateMap<UploadFileInfoDto, UserProfileMediaFile>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.OwnerId));
        
        CreateMap<UserProfileMediaFile, UserProfilePictureDto>()
            .ForMember(dest => dest.ImageUrl,
                opt => opt.ConvertUsing<StorageFileToUrlConverter, StorageFile>(src => src.StorageFile))
            .ReverseMap();
    }
}