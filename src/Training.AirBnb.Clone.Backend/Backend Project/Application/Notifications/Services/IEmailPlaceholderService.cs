using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Notifications.Services;
public interface IEmailPlaceholderService
{
    ValueTask<Dictionary<string, string>> GetTemplateValues(Guid userId, EmailTemplate emailTemplate);
}