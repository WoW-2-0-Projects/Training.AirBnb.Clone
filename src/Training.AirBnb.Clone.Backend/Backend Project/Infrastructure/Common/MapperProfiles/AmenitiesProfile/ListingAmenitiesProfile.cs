using AutoMapper;
using Backend_Project.Application.Amenities;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.AmenitiesProfile;

public class ListingAmenitiesProfile : Profile
{
    public ListingAmenitiesProfile()
    {
        CreateMap<ListingAmenities, ListingAmenitiesDto>();
        CreateMap<ListingAmenitiesDto, ListingAmenities>();
    }
}