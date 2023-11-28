using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Profiles;

/// <summary>
/// AutoMapper profile for mapping between the SmsTemplate and SmsTemplateDto classes.
/// </summary>
public class SmsTemplateProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserProfile
    /// </summary>
    public SmsTemplateProfile()
    {
         CreateMap<SmsTemplate, SmsTemplateDTO>().ReverseMap();
    }
}