using AutoMapper;
using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.ListingCategoryDetailsProfile;

public class ListingFeatureProfile : Profile
{
    public ListingFeatureProfile()
    {
        CreateMap<ListingFeature, ListingFeatureDto>();
        CreateMap<ListingFeatureDto, ListingFeature>();
    }
}