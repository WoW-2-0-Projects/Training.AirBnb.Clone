using AirBnB.Application.StorageFiles.Models;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class FormFileMapper : Profile
{
    public FormFileMapper()
    {
        CreateMap<IFormFile, UploadFileInfoDto>()
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.OpenReadStream()))
            .ForMember(dest => dest.Size, opt => opt.MapFrom(src => src.Length));
    }
}