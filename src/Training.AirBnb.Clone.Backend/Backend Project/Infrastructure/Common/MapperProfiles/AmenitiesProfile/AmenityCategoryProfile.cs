using AutoMapper;
using Backend_Project.Application.Amenities;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.AmenitiesProfile;

public class AmenityCategoryProfile : Profile
{
    public AmenityCategoryProfile()
    {
        CreateMap<AmenityCategory, AmenityCategoryDto>();
        CreateMap<AmenityCategoryDto, AmenityCategory>();
    }
}