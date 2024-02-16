using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between SmsHistory and SmsMessage
/// </summary>
public class SmsHistoryMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the SmsHistoryMapper class.
    /// </summary>
    public SmsHistoryMapper()
    {
        CreateMap<SmsHistory, SmsMessage>().ReverseMap();
    }
}
