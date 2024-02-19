using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Identity.Events;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Enums;

namespace AirBnB.Infrastructure.Common.Identity.EventHandlers;

public class UserCreatedEventHandler(IEventBusBroker eventBusBroker) : IEventHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Create welcome notification event.
        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = notification.CreatedUser.Id,
            TemplateType = NotificationTemplateType.SystemWelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, notification.CreatedUser.FirstName }
            }
        };
        
        // send welcome email
        await eventBusBroker.PublishAsync(
            welcomeNotificationEvent, 
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName, 
            cancellationToken);
    }
}