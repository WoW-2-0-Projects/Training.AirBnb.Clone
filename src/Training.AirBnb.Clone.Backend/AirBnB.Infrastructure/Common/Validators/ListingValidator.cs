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
                RuleFor(listing => listing.Id).NotEqual(Guid.Empty);
            
            RuleFor(listing => listing.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(128).WithMessage("Name cannot exceed 256 characters.");

            RuleFor(listing => listing.BuiltDate).LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

            RuleFor(listing => listing.PricePerNight)
                .NotEmpty().WithMessage("Price is required.");
        });
    }
}