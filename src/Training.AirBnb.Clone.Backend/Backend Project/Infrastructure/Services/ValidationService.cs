using Backend_Project.Application.Validation;
using System.Text.RegularExpressions;

namespace Backend_Project.Infrastructure.Services;

public class ValidationService : IValidationService
{
    private const string _emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";

    public bool IsValidEmailAddress(string emailAddress) =>
        !string.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress, _emailPattern);

    public bool IsValidNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
            return false;
        for (int index = 0; index < name.Length; index++)
        {
            if (char.IsLetter(name[index]) || index > 0
                && (name[index - 1].ToString().Equals("o", StringComparison.OrdinalIgnoreCase)
                || name[index - 1].ToString().Equals("g", StringComparison.OrdinalIgnoreCase))
                && (name[index].ToString().Equals("'") || name[index] == '`'))
                continue;
            else return false;
        }
        return true;
    }
}