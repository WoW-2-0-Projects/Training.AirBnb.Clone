using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

public class GuestFeedbackMapper : Profile
{
    public GuestFeedbackMapper()
    {
        CreateMap<GuestFeedback, GuestFeedbackDto>().ForMember(dest => dest.GuestName,
            opt => opt
                .MapFrom(src => src.Guest.FirstName));

        CreateMap<GuestFeedbackDto, GuestFeedback>();
    }
}