namespace AirBnB.Domain.Enums;

/// <summary>
/// Enumerates the different themes that can be used.
/// </summary>
public enum Theme
{
    /// <summary>
    /// Represents the system-defined theme, which adapts to the system's default.
    /// </summary>
    SystemTheme = 0,

    /// <summary>
    /// Represents the light theme, which typically has a brighter color scheme.
    /// </summary>
    Light = 2,

    /// <summary>
    /// Represents the dark theme, which typically has a darker color scheme.
    /// </summary>
    Dark = 1,
}
