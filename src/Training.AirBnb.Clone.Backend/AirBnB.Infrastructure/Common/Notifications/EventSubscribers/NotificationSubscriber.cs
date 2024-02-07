using AirBnB.Application.Common.EventBus.Brokers;
using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Serializers;
using AirBnB.Domain.Constants;
using AirBnB.Infrastructure.Common.EventBus.Services;
using AirBnB.Infrastructure.Common.Notifications.Settings;
using AirBnB.Infrastructure.Common.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AirBnB.Infrastructure.Common.Notifications.EventSubscribers;

public class NotificationSubscriber(
    IRabbitMqConnectionProvider rabbitMqConnectionProvider,
    IOptions<NotificationSubscriberSettings> eventBusSubscriberSettings,
    IJsonSerializationSettingsProvider jsonSerializationSettingsProvider,
    IEventBusBroker eventBusBroker
) : EventSubscriber<NotificationEvent>(
    rabbitMqConnectionProvider,
    eventBusSubscriberSettings,
    [EventBusConstants.ProcessNotificationQueueName, EventBusConstants.RenderNotificationQueueName, EventBusConstants.SendNotificationQueueName],
    jsonSerializationSettingsProvider
)
{
    protected override async ValueTask SetChannelAsync()
    {
        await base.SetChannelAsync();

        await Channel.ExchangeDeclareAsync(EventBusConstants.NotificationExchangeName, ExchangeType.Direct, true);

        await Channel.QueueDeclareAsync(EventBusConstants.ProcessNotificationQueueName, true, false, false);
        await Channel.QueueDeclareAsync(EventBusConstants.RenderNotificationQueueName, true, false, false);
        await Channel.QueueDeclareAsync(EventBusConstants.SendNotificationQueueName, true, false, false);

        await Channel.QueueBindAsync(
            EventBusConstants.ProcessNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName
        );

        await Channel.QueueBindAsync(
            EventBusConstants.RenderNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.RenderNotificationQueueName
        );

        await Channel.QueueBindAsync(
            EventBusConstants.SendNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.SendNotificationQueueName
        );
    }
    
    protected override async ValueTask<(bool Result, bool Redeliver)> ProcessAsync(NotificationEvent @event, CancellationToken cancellationToken)
    {
        await eventBusBroker.PublishLocalAsync(@event);

        return (true, false);
    }
}