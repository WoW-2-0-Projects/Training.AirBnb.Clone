namespace Backend_Project.Application.Notifications.Services
{
    public interface IEmailManagementService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}