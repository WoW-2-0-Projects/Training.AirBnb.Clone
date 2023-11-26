namespace AirBnB.Application.Common.Settings;

public class ValidationSettings
{
    public string EmailRegexPattern { get; set; } = default!;

    public string NameRegexPattern { get; set; } = default!;

    public string PasswordRegexPattern { get; set; } = default!;
}