using AirBnB.Application.Common.Identity.Models;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Infrastructure.StorageFiles.Mappers;

public class UserMappers : Profile
{
    public UserMappers()
    {
        CreateMap<SignUpDetails, User>();
    }
}