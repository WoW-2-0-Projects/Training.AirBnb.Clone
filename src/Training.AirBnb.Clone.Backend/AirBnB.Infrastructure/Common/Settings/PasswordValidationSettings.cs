namespace AirBnB.Infrastructure.Common.Settings;

/// <summary>
/// Configures the desired requirements for password validation.
/// </summary>
public class PasswordValidationSettings
{
    // Must contain at least one digit
    public bool RequireDigit { get; set; }
   
    // Must contain at least one uppercase letter
    public bool RequireUppercase { get; set; }
   
    // Must contain at least one lowercase letter
    public bool RequireLowercase { get; set; }
   
    // Must contain at least one non-alphanumeric character
    public bool RequireNonAlphanumeric { get; set; }
    
    // Minimum allowed length
    public int MinimumLength { get; set; }
   
    // Maximum allowed length (default: typically 128)
    public int MaximumLength { get; set; }
}