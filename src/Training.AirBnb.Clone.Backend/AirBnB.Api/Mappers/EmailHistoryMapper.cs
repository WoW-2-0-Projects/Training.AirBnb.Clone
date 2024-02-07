using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Domain.Entities;
using AutoMapper;

namespace AirBnB.Api.Mappers;

/// <summary>
/// AutoMapper profile for mapping between EmailMessage and EmailHistory
/// </summary>
public class EmailHistoryMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the EmailHistory class.
    /// </summary>
    public EmailHistoryMapper()
    {
        CreateMap<EmailMessage, EmailHistory>().ReverseMap();
    }
}
