using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using FluentValidation;

namespace AirBnB.Infrastructure.Common.Validators;

public class GuestFeedbackValidator : AbstractValidator<GuestFeedback>
{
    public GuestFeedbackValidator()
    {
        RuleSet(EntityEvent.OnCreate.ToString(), () =>
        {
            RuleFor(feedback => feedback.ListingId)
                .NotNull()
                .NotEmpty();

            RuleFor(feedback => feedback.Comment)
                .NotNull()
                .NotEmpty()
                .MaximumLength(2000);

            RuleFor(feedback => feedback.Accuracy).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
            RuleFor(feedback => feedback.Value).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
            RuleFor(feedback => feedback.Cleanliness).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
            RuleFor(feedback => feedback.Communication).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
            RuleFor(feedback => feedback.Location).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
            RuleFor(feedback => feedback.CheckIn).LessThanOrEqualTo((byte)5).NotEmpty().NotNull();
        });
    }
}