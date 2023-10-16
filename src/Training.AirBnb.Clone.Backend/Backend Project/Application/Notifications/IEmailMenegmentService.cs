namespace Backend_Project.Application.Notifications
{
    public interface IEmailMenegmentService
    {
        ValueTask<bool> SendEmailAsync(Guid userId, Guid templateId);
    }
}
