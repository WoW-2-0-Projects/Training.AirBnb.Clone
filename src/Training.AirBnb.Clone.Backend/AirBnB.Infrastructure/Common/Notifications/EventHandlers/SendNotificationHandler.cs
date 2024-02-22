using AirBnB.Application.Common.Notifications.Events;
using AirBnB.Application.Common.Notifications.Models;
using AirBnB.Application.Common.Notifications.Services;
using AirBnB.Domain.Common.Events;
using AirBnB.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AirBnB.Infrastructure.Common.Notifications.EventHandlers;

public class SendNotificationHandler(
    IServiceScopeFactory serviceScopeFactory, 
    IMapper mapper) 
    : IEventHandler<SendNotificationEvent>
{
    public async Task Handle(SendNotificationEvent notification, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailSenderService = scope.ServiceProvider.GetRequiredService<IEmailSenderService>();
        var emailHistoryService = scope.ServiceProvider.GetRequiredService<IEmailHistoryService>();
        
        if (notification.Message is EmailMessage emailMessage)
        {
            await emailSenderService.SendAsync(emailMessage, cancellationToken);

            var history = mapper.Map<EmailHistory>(emailMessage);
            history.SenderUserId = notification.SenderUserId;
            history.ReceiverUserId = notification.ReceiverUserId;

            await emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            if (!history.IsSuccessful) throw new InvalidOperationException("Email history is not created");
        }
    }
}