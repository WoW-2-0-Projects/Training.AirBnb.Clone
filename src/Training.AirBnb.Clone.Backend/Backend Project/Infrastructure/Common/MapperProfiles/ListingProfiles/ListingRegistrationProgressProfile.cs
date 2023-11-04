using AutoMapper;
using Backend_Project.Application.Listings.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.ListingProfiles;

public class ListingRegistrationProgressProfile : Profile
{
    public ListingRegistrationProgressProfile()
    {
        CreateMap<ListingRegistrationProgress, RegistrationProgressDto>();
        CreateMap<RegistrationProgressDto, ListingRegistrationProgress>();
    }
}