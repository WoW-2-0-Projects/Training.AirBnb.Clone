using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.StorageFiles.Validators;

public class ListingMediaFileValidator : AbstractValidator<ListingMediaFile>
{
    public ListingMediaFileValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () =>
        {
            RuleFor(media => media.OrderNumber)
                .GreaterThanOrEqualTo(byte.MinValue).WithMessage("Invalid order number.")
                .LessThanOrEqualTo((byte)100).WithMessage("Listing Images count can't exceed 100");

            RuleFor(media => media.ListingId).NotNull().NotEmpty().NotEqual(Guid.Empty);
        });
    }
}