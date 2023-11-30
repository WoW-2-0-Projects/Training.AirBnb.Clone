using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplate> 
{
    public EmailTemplateValidator()
    {
        RuleFor(template => template.Content)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(129_536);

        RuleFor(template => template.Type)
            .Equal(NotificationType.Email);

        RuleFor(emailTemplate => emailTemplate.Subject)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(129_536);
    }
}