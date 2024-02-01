using System.Collections.Immutable;
using System.Text;
using AirBnB.Application.Common.Identity.Models;
using AirBnB.Application.Common.Identity.Services;
using AirBnB.Domain.Entities;
using AirBnB.Domain.Extension;
using AirBnB.Infrastructure.Common.Settings;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AirBnB.Infrastructure.Common.Identity.Services;

/// <summary>
/// Provides services for generating and validating passwords.
/// </summary>
public class PasswordGeneratorService(
    IOptions<PasswordValidationSettings> passwordValidationSettings,
    IValidator<CredentialDetails> credentialDetailsValidator
    ) : IPasswordGeneratorService
{
    private readonly PasswordValidationSettings _passwordValidationSettings = passwordValidationSettings.Value;
    private readonly Random _random = new();

    /// <summary>
    /// Generates a password that adheres to specified validation settings.
    /// </summary>
    /// <returns>A randomly generated password string.</returns>
    public string GeneratePassword()
    {
        // Create a StringBuilder to efficiently construct the password
        var password = new StringBuilder();

        // Determine the required password elements based on settings
        var requiredElements = GetRequiredElements();
        
        // Add at least one of each required element to the password
        requiredElements.ForEach(element => password.Append(GetPasswordElement(element)));
        
        // Extend the password with additional random elements until it meets the minimum length requirement
        while (password.Length < _passwordValidationSettings.MinimumLength)
            password.Append(GetPasswordElement((PasswordElementType)_random.Next(0, requiredElements.Count - 1)));

        // Randomize the order of characters in the password for enhanced security
        var randomizedPassword = password.ToString().ToCharArray();
        _random.Shuffle(randomizedPassword);
        
        return new string(randomizedPassword);
    }
    
    /// <summary>
    /// Validates a password against specified rules and personal information, ensuring its strength and security.
    /// </summary>
    /// <param name="password">The password to be validated.</param>
    /// <param name="user">The user object containing relevant personal information.</param>
    /// <returns>The validated password if it meets all requirements.</returns>
    /// <exception cref="ValidationException">Thrown if the password fails validation.</exception>
    public string GetValidatedPassword(string password, User user)
    {
        // Create a validation context to pass to the validator
        var validationContext = new ValidationContext<CredentialDetails>(
            new CredentialDetails
            {
                Password = password
            }
        )
        {
            // Include personal information in the validation context for potential checks
            RootContextData =
            {
                ["PersonalInformation"] = new[] { user.FirstName, user.LastName, user.EmailAddress }
            }
        };
        
        // Perform validation using the configured validator
        var validationResult = credentialDetailsValidator.Validate(validationContext);
        
        // If validation fails, throw a descriptive exception
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return password;
    }

    /// <summary>
    /// Determines the password elements that are required based on current validation settings.
    /// </summary>
    /// <returns>An immutable list of required password elements.</returns>
    private ImmutableList<PasswordElementType> GetRequiredElements()
    {
        // Create a list to hold the required elements
        var requiredElements = new List<PasswordElementType>();

        // Check each validation setting and add corresponding elements if required
        if (_passwordValidationSettings.RequireDigit)
            requiredElements.Add(PasswordElementType.Digit);

        if (_passwordValidationSettings.RequireUppercase)
            requiredElements.Add(PasswordElementType.Uppercase);

        if (_passwordValidationSettings.RequireLowercase)
            requiredElements.Add(PasswordElementType.Lowercase);

        if (_passwordValidationSettings.RequireNonAlphanumeric)
            requiredElements.Add(PasswordElementType.NonAlphanumeric);

        return requiredElements.ToImmutableList();
    }

    /// <summary>
    /// Generates a random character of a specified password element type.
    /// </summary>
    /// <param name="passwordElementType">The type of password element to generate.</param>
    /// <returns>A randomly generated character of the specified type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if an invalid password element type is provided.</exception>
    private char GetPasswordElement(PasswordElementType passwordElementType)
    {
        return passwordElementType switch
        {
            PasswordElementType.Digit => CharExtensions.GetRandomDigit(_random),
            PasswordElementType.Uppercase => CharExtensions.GetRandomUppercase(_random),
            PasswordElementType.Lowercase => CharExtensions.GetRandomLowercase(_random),
            PasswordElementType.NonAlphanumeric => CharExtensions.GetRandomNonAlphanumeric(_random),
            _ => throw new ArgumentOutOfRangeException(nameof(passwordElementType), passwordElementType, null)
        };
    }
    
    /// <summary>
    /// Specifies the different types of elements that can constitute a password.
    /// </summary>
    private enum PasswordElementType
    {
        Digit = 0,
        Uppercase = 1,
        Lowercase = 2,
        NonAlphanumeric = 3
    }
}