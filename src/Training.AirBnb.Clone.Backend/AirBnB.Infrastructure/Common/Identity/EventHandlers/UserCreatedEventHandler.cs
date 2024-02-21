using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Identity.Events;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Verifications.Services;
using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Constants;
using AirBnB.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace AirBnB.Infrastructure.Common.Identity.EventHandlers;

public class UserCreatedEventHandler(IEventBusBroker eventBusBroker, IServiceScopeFactory serviceScopeFactory) : IEventHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userInfoVerificationCodeService = scope.ServiceProvider.GetRequiredService<IUserInfoVerificationCodeService>();

        // Create welcome notification event
        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = notification.CreatedUser.Id,
            TemplateType = NotificationTemplateType.SystemWelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, notification.CreatedUser.FirstName }
            }
        };

        // Create verification notification event
        var verificationCode = await userInfoVerificationCodeService.CreateAsync(
            VerificationCodeType.EmailAddressVerification,
            notification.CreatedUser.Id,
            cancellationToken
        );

        var verificationNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = notification.CreatedUser.Id,
            TemplateType = NotificationTemplateType.EmailVerificationNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.EmailAddressVerificationCodePlaceholder, verificationCode.Code }
            }
        };

        // Send welcome email
        await eventBusBroker.PublishAsync(
            welcomeNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        // Send verification email
        await eventBusBroker.PublishAsync(
            verificationNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );
    }
}