using Backend_Project.Domain.Entities;

namespace Backend_Project.Application.Interfaces;

public interface IEmailSenderService
{
    ValueTask<bool> SendEmailAsync(EmailMessage emailMessage);
}
