using AutoMapper;
using Backend_Project.Application.ListingCategoryDetails.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.ListingCategoryDetailsProfile;

public class ListingCategoryProfile : Profile
{
    public ListingCategoryProfile()
    {
        CreateMap<ListingCategory, ListingCategoryDto>();
        CreateMap<ListingCategoryDto, ListingCategory>();
    }
}