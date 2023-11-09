using Backend_Project.Application.Validation.Services;
using Backend_Project.Application.Validation.Settins;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace Backend_Project.Infrastructure.Services.ValidationServices;

public class ValidationService : IValidationService
{
    private readonly ValidationSettings _settings;

    public ValidationService(IOptions<ValidationSettings> settings)
    {
        _settings = settings.Value;
    }

    public bool IsValidEmailAddress(string emailAddress) =>
        !string.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress, _settings.EmailPattern, RegexOptions.None, TimeSpan.FromSeconds(1));

    public bool IsValidNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
            return false;
        for (int index = 0; index < name.Length; index++)
        {
            if (!(char.IsLetter(name[index]) || index > 0
                && (name[index - 1].ToString().Equals("o", StringComparison.OrdinalIgnoreCase)
                || name[index - 1].ToString().Equals("g", StringComparison.OrdinalIgnoreCase))
                && (name[index].ToString().Equals("'") || name[index] == '`')))
                return false;
        }
        return true;
    }
}