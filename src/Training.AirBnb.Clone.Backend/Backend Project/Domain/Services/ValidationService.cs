
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace Backend_Project.Domain.Services;

public class ValidationService : IValidationService
{
    private const string _emailPattern = @"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    public bool IsValidEmailAddress(string emailAddress) => 
        !string.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress,_emailPattern);

    public ValueTask<bool> IsValidNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
            return new ValueTask<bool>(false);
        for (int index = 0; index < name.Length; index++)
        {
            if (char.IsLetter(name[index]) || (index > 0
                && (name[index - 1].ToString().Equals("o", StringComparison.OrdinalIgnoreCase)
                || name[index - 1].ToString().Equals("g", StringComparison.OrdinalIgnoreCase))
                && (name[index].ToString().Equals("'") || name[index] == '`')))
                continue;
            else return new ValueTask<bool>(false);
        }
        return new ValueTask<bool>(true);
    }

    public bool IsValidPhoneNumber(string phoneNumber)
    {
        throw new NotImplementedException();
    }
}
