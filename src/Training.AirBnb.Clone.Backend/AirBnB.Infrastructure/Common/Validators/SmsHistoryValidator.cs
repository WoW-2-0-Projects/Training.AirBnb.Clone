using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

public class SmsHistoryValidator : AbstractValidator<SmsHistory>
{
    public SmsHistoryValidator()
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(history => history.Type)
                    .Equal(NotificationType.Sms);

                RuleFor(history => history.SenderPhoneNumber)
                    .NotEmpty();

                RuleFor(history => history.ReceiverPhoneNumber)
                    .NotEmpty();

                RuleFor(history => history.Content)
                    .NotEmpty();
            }
        );
    }
}
