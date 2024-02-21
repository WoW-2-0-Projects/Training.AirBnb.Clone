using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AirBnB.Infrastructure.Common.Notifications.EventHandlers;

public class ProcessNotificationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IEventBusBroker eventBusBroker) 
    : IEventHandler<ProcessNotificationEvent>
{
    public async Task Handle(ProcessNotificationEvent notification, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
        var processNotificationEventValidator = scope.ServiceProvider.GetRequiredService<IValidator<ProcessNotificationEvent>>();
        
        var validationResult = await processNotificationEventValidator.ValidateAsync(notification, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var senderUser = notification.SenderUserId != Guid.Empty
            ? await userService.GetByIdAsync(notification.SenderUserId, cancellationToken: cancellationToken)
            : await userService.GetSystemUserAsync(true, cancellationToken);
        
        var receiverUser = await userService.Get(user => user.Id == notification.ReceiverUserId, asNoTracking: true)
            .Include(user => user.UserSettings)
            .FirstAsync(cancellationToken);

        // If notification provider type is not specified, get from receiver user settings
        notification.Type ??= receiverUser!.UserSettings.PreferredNotificationType;

        var renderNotificationEvent = new RenderNotificationEvent
        {
            SenderUserId = senderUser!.Id,
            ReceiverUserId = receiverUser.Id,
            Template = (await emailTemplateService.GetByTypeAsync(notification.TemplateType, cancellationToken: cancellationToken))!,
            SenderUser = senderUser,
            ReceiverUser = receiverUser,
            Variables = notification.Variables ?? new Dictionary<string, string>()
        };

        await eventBusBroker.PublishAsync(
            renderNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.RenderNotificationQueueName,
            cancellationToken
        );
    }
}