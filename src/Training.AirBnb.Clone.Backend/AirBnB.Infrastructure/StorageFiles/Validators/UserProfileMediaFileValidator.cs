using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.StorageFiles.Validators;

public class UserProfileMediaFileValidator : AbstractValidator<UserProfileMediaFile>
{
    public UserProfileMediaFileValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () =>
        {
            RuleFor(media => media.UserId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        });
    }
}