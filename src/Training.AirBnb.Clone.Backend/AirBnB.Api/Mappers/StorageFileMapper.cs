using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class StorageFileMapper : Profile
{
    public StorageFileMapper()
    {
        CreateMap<StorageFile, StorageFileDto>().ReverseMap();
    }
}