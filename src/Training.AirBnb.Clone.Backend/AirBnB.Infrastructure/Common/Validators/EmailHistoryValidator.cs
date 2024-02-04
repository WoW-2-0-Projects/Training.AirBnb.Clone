using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

public class EmailHistoryValidator : AbstractValidator<EmailHistory>
{
    public EmailHistoryValidator()
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(history => history.SenderEmailAddress)
                    .NotEmpty();

                RuleFor(history => history.ReceiverEmailAddress)
                    .NotEmpty();

                RuleFor(history => history.Subject)
                    .NotEmpty();

                RuleFor(history => history.Content)
                    .NotEmpty();

                RuleFor(history => history.Type)
                    .Equal(NotificationType.Email);
            }
        );
    }
}
