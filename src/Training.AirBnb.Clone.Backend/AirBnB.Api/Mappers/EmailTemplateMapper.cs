using AirBnB.Api.Models.DTOs;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// Mapper class for mapping between EmailTemplate and EmailTemplateDto.
/// </summary>
public class EmailTemplateMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the EmailTemplateMapper class.
    /// </summary>
    public EmailTemplateMapper()
    {
        CreateMap<EmailTemplate, EmailTemplateDto>().ReverseMap();
    }
}