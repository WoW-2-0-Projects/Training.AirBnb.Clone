using AirBnB.Application.Common.Settings;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Validators;

public class StorageFileValidator : AbstractValidator<StorageFile>
{
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