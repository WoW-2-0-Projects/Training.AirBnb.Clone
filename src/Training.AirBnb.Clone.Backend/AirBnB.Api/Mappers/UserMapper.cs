using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between the User and UserDto classes.
/// </summary>
public class UserMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserMapper
    /// </summary>
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}