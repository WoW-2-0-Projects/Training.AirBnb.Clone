namespace AirBnB.Application.Ratings.Services;

/// <summary>
/// Represents listing ratings recalculation service
/// </summary>
public interface IRatingRecalculationService
{
    /// <summary>
    /// Recalculates listings ratings that received new feedbacks
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public ValueTask RecalculateListingsRatings();
}