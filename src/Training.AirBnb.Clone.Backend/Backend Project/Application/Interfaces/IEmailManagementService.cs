
namespace Backend_Project.Application.Interfaces
{
    public interface IEmailManagementService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}
