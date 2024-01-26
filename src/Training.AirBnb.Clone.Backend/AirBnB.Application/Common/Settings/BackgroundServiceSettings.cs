namespace AirBnB.Application.Common.Settings;

/// <summary>
/// Represents the settings for a background service.
/// </summary>
public abstract class BackgroundServiceSettings
{
    /// <summary>
    /// Gets or sets the time interval between each execution of the background service, specified in seconds.
    /// </summary>
    public int ExecutionIntervalInSeconds { get; init; }
}