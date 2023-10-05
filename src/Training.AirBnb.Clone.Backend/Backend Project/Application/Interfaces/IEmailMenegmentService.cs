
namespace Backend_Project.Application.Interfaces
{
    public interface IEmailMenegmentService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}
