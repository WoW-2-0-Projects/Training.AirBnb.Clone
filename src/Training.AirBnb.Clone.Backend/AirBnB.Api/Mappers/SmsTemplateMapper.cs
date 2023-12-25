using AirBnB.Api.Models.DTOs.Notification;
using AirBnB.Domain.Entities.Notification;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// Mapper class for mapping between SmsTemplate and SmsTemplateDto.
/// </summary>
public class SmsTemplateMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the SmsTemplateMapper class.
    /// </summary>
    public SmsTemplateMapper()
    {
        CreateMap<SmsTemplate, SmsTemplateDto>().ReverseMap();
    }
}