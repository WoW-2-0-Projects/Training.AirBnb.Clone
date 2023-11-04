using Backend_Project.Domain.Entities;
using Backend_Project.Application.Notifications;
using Backend_Project.Application.Foundations.AccountServices;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailMessageSevice : IEmailMessageService
{
    private readonly IUserService _userService;

    public EmailMessageSevice(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<EmailMessage> ConvertToMessage(EmailTemplate emailTemplate, IEnumerable<KeyValuePair<string, string>> values, Guid senderUserId, Guid receiverUserId)
    {
        var senderUser = await _userService.GetByIdAsync(senderUserId);
        var receiverUser = await _userService.GetByIdAsync(receiverUserId);

        var body = emailTemplate.Body;
        var subject = emailTemplate.Subject;
        
        foreach (var item in values)
        {
            subject = subject.Replace(item.Key, item.Value);
            body = body.Replace(item.Key, item.Value);
        }

        var emailMessage = new EmailMessage(subject, body, senderUserId, receiverUserId, senderUser.EmailAddress, receiverUser.EmailAddress);

        return emailMessage;
    }
}