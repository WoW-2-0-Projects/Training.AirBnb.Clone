using AirBnB.Application.Common.EventBus.Brokers;
using Microsoft.Extensions.Hosting;

namespace AirBnB.Infrastructure.Common.EventBus.Services;

public class EventBusBackgroundService(IEnumerable<IEventSubscriber> eventSubscribers) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.WhenAll(eventSubscribers.Select(eventSubscriber => eventSubscriber.StartAsync(stoppingToken).AsTask()));
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.WhenAll(eventSubscribers.Select(eventSubscriber => eventSubscriber.StopAsync(cancellationToken).AsTask()));
    }
    
}