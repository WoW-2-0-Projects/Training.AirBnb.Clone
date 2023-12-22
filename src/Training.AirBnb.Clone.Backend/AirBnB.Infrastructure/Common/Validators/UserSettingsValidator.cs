using AirBnB.Domain.Entities;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating user settings data using fluent validation
/// </summary>
public class UserSettingsValidator : AbstractValidator<UserSettings>
{
    public UserSettingsValidator()
    {
        RuleFor(userSettings => userSettings.Id).NotEqual(Guid.Empty);
        RuleFor(userSettings => userSettings.PreferredTheme).NotEmpty().NotNull();
    }
}
