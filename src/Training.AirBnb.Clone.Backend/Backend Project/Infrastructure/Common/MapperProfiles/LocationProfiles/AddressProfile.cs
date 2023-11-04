using AutoMapper;
using Backend_Project.Application.Location.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.LocationProfiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
    }
}