﻿using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between the EmailTemplate and EmailTemplateDto classes.
/// </summary>
public class EmailTemplateMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the UserProfile
    /// </summary>
    public EmailTemplateMapper()
    {
        CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
    }
}