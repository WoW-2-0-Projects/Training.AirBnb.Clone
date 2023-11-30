using AirBnB.Api.Models.Dtos;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Profiles;

/// <summary>
/// AutoMapper profile for mapping between the SmsTemplate and SmsTemplateDto classes.
/// </summary>
public class SmsTemplateMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserProfile
    /// </summary>
    public SmsTemplateMapper()
    {
         CreateMap<SmsTemplate, SmsTemplateDto>().ReverseMap();
    }
}