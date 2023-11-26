﻿namespace AirBnB.Application.Common.Settings;

/// <summary>
/// Represents settings for user data validation.
/// </summary>
public class ValidationSettings
{
    /// <summary>
    /// Gets or sets the regular expression pattern for validating email addresses.
    /// </summary>
    public string EmailRegexPattern { get; set; } = default!;

    /// <summary>
    /// Gets or sets the regular expression pattern for validating names.
    /// </summary>
    public string NameRegexPattern { get; set; } = default!;

    /// <summary>
    /// Gets or sets the regular expression pattern for validating passwords.
    /// </summary>
    public string PasswordRegexPattern { get; set; } = default!;
}