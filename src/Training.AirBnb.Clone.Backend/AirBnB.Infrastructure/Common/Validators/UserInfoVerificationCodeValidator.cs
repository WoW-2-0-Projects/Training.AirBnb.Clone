using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Infrastructure.Common.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

/// <summary>
/// Validator for user information verification codes.
/// </summary>
public class UserInfoVerificationCodeValidator : AbstractValidator<UserInfoVerificationCode>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInfoVerificationCodeValidator"/> class.
    /// </summary>
    /// <param name="verificationSettings">The verification code settings.</param>
    /// <param name="validationSettings">The validation settings.</param>
    public UserInfoVerificationCodeValidator(IOptions<VerificationCodeSettings> verificationSettings, IOptions<ValidationSettings> validationSettings)
    {
        var verificationSettingsValue = verificationSettings.Value;
        var validationSettingsValue = validationSettings.Value;

        RuleSet(EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(code => code.UserId).NotEqual(Guid.Empty);

                RuleFor(code => code.ExpiryTime)
                    .GreaterThanOrEqualTo(DateTimeOffset.UtcNow)
                    .LessThanOrEqualTo(DateTimeOffset.UtcNow.AddSeconds(verificationSettingsValue.VerificationCodeExpiryTimeInSeconds));

                RuleFor(code => code.IsActive).Equal(true);

                RuleFor(code => code.VerificationLink).NotEmpty().Matches(validationSettingsValue.UrlRegexPattern);
            });
    }
}