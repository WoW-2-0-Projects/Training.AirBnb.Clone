using AirBnB.Application.Common.Notifications.Events;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Notifications.Validators;

public class ProcessNotificationEventValidator : AbstractValidator<ProcessNotificationEvent>
{
    public ProcessNotificationEventValidator()
    {
        RuleFor(history => history.ReceiverUserId).NotEqual(Guid.Empty);
    }
}