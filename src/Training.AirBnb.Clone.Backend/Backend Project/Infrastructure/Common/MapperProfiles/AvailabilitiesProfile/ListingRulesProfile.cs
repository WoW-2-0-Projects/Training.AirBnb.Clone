using AutoMapper;
using Backend_Project.Application.Availabilities.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.AvailabilitiesProfile;

public class ListingRulesProfile : Profile
{
    public ListingRulesProfile()
    {
        CreateMap<ListingRules, ListingRulesRegistrationDto>().ReverseMap();
        CreateMap<ListingRules, ListingRulesDto>().ReverseMap();
    }
}