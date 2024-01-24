namespace AirBnB.Application.Common.Settings;

/// <summary>
/// Represents the settings for a background service.
/// </summary>
public class BackgroundServiceSettings
{
    /// <summary>
    /// Gets or sets the time interval between each execution of the Listing Rating recalculation process, specified in hours.
    /// </summary>
    public int ListingRatingRecalculationIntervalInHours { get; set; }
}