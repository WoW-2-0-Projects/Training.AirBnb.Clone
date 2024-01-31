using System.ComponentModel.DataAnnotations;
using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

public class UserCredentialsValidator : AbstractValidator<UserCredentials>
{
    public UserCredentialsValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;
        
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(userCredentials => userCredentials.PasswordHash)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.PasswordRegexPattern);
            });
    }
}