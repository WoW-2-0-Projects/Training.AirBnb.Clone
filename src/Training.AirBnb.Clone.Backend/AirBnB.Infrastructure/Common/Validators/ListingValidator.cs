using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

/// <summary>
/// Validator for the Listing entity.
/// </summary>
public class ListingValidator : AbstractValidator<Listing>
{
    /// <summary>
    /// Initializes a new instance of the ListingValidator class.
    /// </summary>
    public ListingValidator()
    {
        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
        {
            RuleFor(listing => listing.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(128).WithMessage("Title cannot exceed 256 characters.");

            RuleFor(listing => listing.DescriptionId)
                .NotEqual(Guid.Empty).WithMessage("DescriptionId is required.");

            RuleFor(listing => listing.PropertyTypeId)
                .NotEqual(Guid.Empty).WithMessage("PropertyTypeId is required.");

            RuleFor(listing => listing.LocationId)
                .NotEqual(Guid.Empty).WithMessage("LocationId is required.");

            RuleFor(listing => listing.RulesId)
                .NotEqual(Guid.Empty).WithMessage("RulesId is required.");

            RuleFor(listing => listing.AvailabilityId)
                .NotEqual(Guid.Empty).WithMessage("AvailabilityId is required.");

            RuleFor(listing => listing.HostId)
                .NotEqual(Guid.Empty).WithMessage("HostId is required.");

            RuleFor(listing => listing.Price)
                .NotEmpty().WithMessage("Price is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");
        });
    }
}