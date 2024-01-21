namespace AirBnB.Infrastructure.Common.Settings;

public class PasswordValidationSettings
{
    public bool RequireDigit { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }
}