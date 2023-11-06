using AutoMapper;
using Backend_Project.Application.Files.Dtos;
using Microsoft.AspNetCore.Http;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.FileProfiles;

public class FileFormProfile : Profile
{
    public FileFormProfile()
    {
        CreateMap<IFormFile, UploadFileDto>()
            .ForMember(dest => dest.Source, opt => opt
                .MapFrom(src => src.OpenReadStream()))
            .ForMember(dest => dest.Size, opt => opt
                .MapFrom(src => src.Length));
    }
}