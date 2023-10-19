namespace Backend_Project.Application.Notifications
{
    public interface IEmailManagementService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}