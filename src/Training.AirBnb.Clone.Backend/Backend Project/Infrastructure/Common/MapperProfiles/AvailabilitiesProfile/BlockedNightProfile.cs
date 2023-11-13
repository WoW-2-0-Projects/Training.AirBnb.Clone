using AutoMapper;
using Backend_Project.Application.Availabilities.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.AvailabilitiesProfile;

public class BlockedNightProfile : Profile
{
    public BlockedNightProfile()
    {
        CreateMap<BlockedNight, BlockedNightDto>().ReverseMap();
    }
}