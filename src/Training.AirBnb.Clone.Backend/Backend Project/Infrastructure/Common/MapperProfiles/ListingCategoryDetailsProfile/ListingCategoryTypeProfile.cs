using AutoMapper;
using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.ListingCategoryDetailsProfile;

public class ListingCategoryTypeProfile : Profile
{
    public ListingCategoryTypeProfile()
    {
        CreateMap<ListingCategoryType,  ListingCategoryTypeDto>();
        CreateMap<ListingCategoryTypeDto,  ListingCategoryType>();
    }
}