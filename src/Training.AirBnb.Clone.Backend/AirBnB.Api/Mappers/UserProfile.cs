using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace AirBnB.Api.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}