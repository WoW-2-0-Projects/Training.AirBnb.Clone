using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between the UserSettings and UserSettingsDto classes.
/// </summary>
public class UserSettingsMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserSettingsMapper
    /// </summary>
    public UserSettingsMapper()
    {
        CreateMap<UserSettings, UserSettingsDto>().ReverseMap();
    }
}