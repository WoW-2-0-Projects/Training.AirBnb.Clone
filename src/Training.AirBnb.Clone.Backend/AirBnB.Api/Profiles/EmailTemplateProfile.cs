using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Profiles;

/// <summary>
/// AutoMapper profile for mapping between the EmailTemplate and EmailTemplateDto classes.
/// </summary>
public class EmailTemplateProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserProfile
    /// </summary>
    public EmailTemplateProfile()
    {
        CreateMap<EmailTemplate, EmailTemplateDTO>().ReverseMap();
    }
}