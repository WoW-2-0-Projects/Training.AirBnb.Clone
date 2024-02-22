using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace AirBnB.Infrastructure.Common.Notifications.EventHandlers;

public class RenderNotificationHandler(
    IServiceScopeFactory serviceScopeFactory,
    IEventBusBroker eventBusBroker)
    : IEventHandler<RenderNotificationEvent>
{
    public async Task Handle(RenderNotificationEvent notification, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailRenderingService = scope.ServiceProvider.GetRequiredService<IEmailRenderingService>();
        
        if (notification.Template.Type == NotificationType.Email)
        {
            var emailMessage = new EmailMessage
            {
                SenderEmailAddress = notification.SenderUser.EmailAddress,
                ReceiverEmailAddress = notification.ReceiverUser.EmailAddress,
                Template = (EmailTemplate)notification.Template,
                Variables = notification.Variables
            };

            await emailRenderingService.RenderAsync(emailMessage, cancellationToken);

            var sendNotificationEvent = new SendNotificationEvent
            {
                SenderUserId = notification.SenderUserId,
                ReceiverUserId = notification.ReceiverUserId,
                Message = emailMessage
            };

            await eventBusBroker.PublishAsync(
                sendNotificationEvent,
                EventBusConstants.NotificationExchangeName,
                EventBusConstants.SendNotificationQueueName,
                cancellationToken
            );
        }
    }
}