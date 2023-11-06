using AutoMapper;
using Backend_Project.Application.Files.Dtos;
using Backend_Project.Application.Listings.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.FileProfiles;

public class ImageInfoProfile : Profile
{
    public ImageInfoProfile()
    {
        CreateMap<UploadFileDto, ImageInfo>();
        CreateMap<ImageInfo, ImageInfoDto>();
    }
}