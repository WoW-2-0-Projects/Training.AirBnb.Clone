using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Notifications;

public interface IEmailSenderService
{
    ValueTask<bool> SendEmailAsync(EmailMessage emailMessage);
}
