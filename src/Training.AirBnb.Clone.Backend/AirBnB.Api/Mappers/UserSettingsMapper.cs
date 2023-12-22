using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class UserSettingsMapper : Profile
{
    public UserSettingsMapper()
    {
        CreateMap<UserSettings, UserSettingsDto>().ReverseMap();
    }
}
