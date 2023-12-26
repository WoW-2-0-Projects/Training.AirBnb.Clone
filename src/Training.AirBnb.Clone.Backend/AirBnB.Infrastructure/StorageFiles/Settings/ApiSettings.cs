namespace AirBnB.Infrastructure.StorageFiles.Settings;

/// <summary>
/// Represents the settings for storage files.
/// </summary>
public class ApiSettings
{
    /// <summary>
    /// Gets the base url for the api.
    /// </summary>
    public string BaseAddress { get; init; } = default!;
}