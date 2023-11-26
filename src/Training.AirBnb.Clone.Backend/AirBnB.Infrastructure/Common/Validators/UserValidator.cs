using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(code => code.Id).NotEqual(Guid.Empty);

                RuleFor(user => user.EmailAddress)
                    .NotEmpty()
                    .MinimumLength(5)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.EmailRegexPattern);

                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern)
                    .WithMessage("First name is not valid");

                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern)
                    .WithMessage("Last name is not valid");

                RuleFor(user => user.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.PasswordRegexPattern);
            }
        );
    }
}