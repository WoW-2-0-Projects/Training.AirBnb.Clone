namespace AirBnB.Infrastructure.Common.Settings;

/// <summary>
/// Represents settings for verification codes.
/// </summary>
public class VerificationCodeSettings
{
    /// <summary>
    /// Gets or sets the default verification link.
    /// </summary>
    public string VerificationLink { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time for verification codes in seconds.
    /// </summary>
    public int VerificationCodeExpiryTimeInSeconds { get; set; }

    /// <summary>
    /// Gets or sets the length of the verification code.
    /// </summary>
    public int VerificationCodeLength { get; set; }
}