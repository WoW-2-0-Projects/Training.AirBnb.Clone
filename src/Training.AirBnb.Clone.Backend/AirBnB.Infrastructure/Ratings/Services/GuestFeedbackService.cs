using System.Linq.Expressions;
using AirBnB.Application.Ratings.Services;
using AirBnB.Domain.Brokers;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Enums;
using AirBnB.Persistence.Repositories.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AirBnB.Infrastructure.Ratings.Services;

public class GuestFeedbackService(
    IValidator<GuestFeedback> guestFeedbacksValidator,
    IGuestFeedbackRepository guestFeedbackRepository,
    IRequestUserContextProvider userContextProvider) 
    : IGuestFeedbackService
{
    public IQueryable<GuestFeedback> Get(
        Expression<Func<GuestFeedback, bool>>? predicate = default, 
        bool asNoTracking = false)
    {
        return guestFeedbackRepository.Get(predicate, asNoTracking);
    }

    public async ValueTask<GuestFeedback?> GetByIdAsync(
        Guid id,
        bool asNoTracking = false, 
        CancellationToken cancellationToken = default)
    {
        return await guestFeedbackRepository.GetByIdAsync(id, asNoTracking, cancellationToken);
    }

    public ValueTask<IList<GuestFeedback>> GetByListingIdAsync(Guid listingId) =>
        new(Get(feedback => feedback.ListingId == listingId)
            .Include(feedback => feedback.Guest)
            .ToList());

    public async ValueTask<GuestFeedback> CreateAsync(
        GuestFeedback guestFeedback, 
        bool saveChanges = true,
        CancellationToken cancellationToken = default)
    {
        guestFeedback.GuestId = userContextProvider.GetUserId();
        
        var validationResult = await guestFeedbacksValidator.ValidateAsync(guestFeedback, options 
            => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()), cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        guestFeedback.OverallRating = (guestFeedback.Accuracy + guestFeedback.Value + guestFeedback.Cleanliness +
                                       guestFeedback.Communication + guestFeedback.Location + guestFeedback.CheckIn) / 6.0;
        
        return await guestFeedbackRepository.CreateAsync(guestFeedback, saveChanges, cancellationToken);
    }
}