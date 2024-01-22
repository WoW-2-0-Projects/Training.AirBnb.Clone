using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating user data using FluentValidation.
/// </summary>
public class UserValidator : AbstractValidator<User>
{
    /// <summary>
    /// The validation settings used for user data validation.
    /// </summary>
    /// <param name="validationSettings"></param>
    public UserValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
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

                RuleFor(user => user.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.NameRegexPattern)
                    .WithMessage("Last name is not valid");

                RuleFor(user => user.PasswordHash)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64);
            }
        );
    }
}