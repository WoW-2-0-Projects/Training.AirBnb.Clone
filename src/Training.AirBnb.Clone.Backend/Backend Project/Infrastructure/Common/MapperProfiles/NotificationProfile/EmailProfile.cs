using AutoMapper;
using Backend_Project.Application.Notifications.Dtos;
using Backend_Project.Domain.Entities;

namespace Backend_Project.Infrastructure.Common.MapperProfiles.NotificationProfile;

public class EmailProfile : Profile
{
    public EmailProfile()
    {
        CreateMap<Email, EmailDto>();
        CreateMap<EmailDto, Email>();
    }
}