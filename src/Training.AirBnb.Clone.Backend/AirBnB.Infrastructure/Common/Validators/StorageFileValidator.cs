using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

/// <summary>
/// Validator class for validating storage file data using FluentValidation.
/// </summary>
public class StorageFileValidator : AbstractValidator<StorageFile>
{
    /// <summary>
    /// The validation settings used for storage file data validation.
    /// </summary>
    /// <param name="validationSettings"></param>
    public StorageFileValidator(IOptions<ValidationSettings> validationSettings)
    {
        var validationSettingsValue = validationSettings.Value;
        
        RuleSet(
            EntityEvent.OnGet.ToString(),
            () =>
            {
                RuleFor(storageFile => storageFile.Id).NotEqual(Guid.Empty);

                RuleFor(storageFile => storageFile.FileName)
                    .NotEmpty()
                    .MaximumLength(64)
                    .Matches(validationSettingsValue.FileNameRegexPattern)
                    .WithMessage("File name is not valid!");

                RuleFor(storageFile => storageFile.Type)
                    .NotEmpty()
                    .IsInEnum();
            }
        );
    }
}