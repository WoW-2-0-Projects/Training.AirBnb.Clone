namespace AirBnB.Infrastructure.Common.Settings;

/// <summary>
/// Configures the desired requirements for password validation.
/// </summary>
public class PasswordValidationSettings
{
    /// <summary>
    /// Determines if a password must contain at least one numerical digit (0-9).
    /// </summary>
    public bool RequireDigit { get; set; }
   
    /// <summary>
    /// Determines if a password must contain at least one uppercase letter (A-Z).
    /// </summary>
    public bool RequireUppercase { get; set; }
   
    /// <summary>
    /// Determines if a password must contain at least one lowercase letter (a-z).
    /// </summary>
    public bool RequireLowercase { get; set; }
   
    /// <summary>
    /// Determines if a password must contain at least one non-alphanumeric character (e.g., symbols like !@#$%^&*).
    /// </summary>
    public bool RequireNonAlphanumeric { get; set; }
    
    /// <summary>
    /// Sets the minimum allowable length for passwords.
    /// </summary>
    public int MinimumLength { get; set; }
   
    /// <summary>
    /// Sets the maximum allowable length for passwords.
    /// </summary>
    public int MaximumLength { get; set; }
}