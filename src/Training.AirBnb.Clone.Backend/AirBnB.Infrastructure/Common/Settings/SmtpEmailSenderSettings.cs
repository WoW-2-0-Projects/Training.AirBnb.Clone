namespace AirBnB.Infrastructure.Common.Settings;

/// <summary>
/// Represents the SMTP email sender settings required for sending emails.
/// </summary>
public class SmtpEmailSenderSettings
{
    /// <summary>
    /// Gets or sets the SMTP host address.
    /// </summary>
    public string Host { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the SMTP port number.
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Gets or sets the email Address used for SMTP authentication.
    /// </summary>
    public string CredentialAddress { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets Password used for SMTP authentication.
    /// </summary>
    public string Password { get; set; } = default!;
}