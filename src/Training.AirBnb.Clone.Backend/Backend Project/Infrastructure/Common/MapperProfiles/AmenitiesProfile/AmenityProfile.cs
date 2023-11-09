using AutoMapper;
using Backend_Project.Application.Amenities.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.AmenitiesProfile;

public class AmenityProfile : Profile
{
    public AmenityProfile()
    {
        CreateMap<Amenity, AmenityDto>();
        CreateMap<AmenityDto, Amenity>();
    }
}