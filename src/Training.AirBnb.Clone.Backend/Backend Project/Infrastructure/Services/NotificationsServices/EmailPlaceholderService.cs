using Backend_Project.Application.Foundations.AccountServices;
using Backend_Project.Application.Notifications;
using Backend_Project.Domain.Entities;
using System.Data;
using System.Text.RegularExpressions;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailPlaceholderService : IEmailPlaceholderService
{
    private readonly IUserService _userService;
    
    private const string _fullName = "{{FullName}}";
    private const string _firstName = "{{FirstName}}";
    private const string _lastName = "{{LastName}}";
    private const string _emailAddress = "{{EmailAddress}}";
    private const string _date = "{{Date}}";
    private const string _companyName = "{{CompanyName}}";
   
    public EmailPlaceholderService(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<Dictionary<string, string>> GetTemplateValues(Guid userId, EmailTemplate emailTemplate)
    {
        var user  = await _userService.GetByIdAsync(userId);
        
        var values = new Dictionary<string, string>();
        
        foreach (var placeholders in GetPlaceholeders(emailTemplate))
        {
            var placeholdersWithValues = placeholders.Select(placeholder =>
            {
                var value = placeholder.Value switch
                {
                    _fullName => (placeholder.Value, $"{user.FirstName} {user.LastName}"),
                    _firstName => (placeholder.Value, user.FirstName),
                    _lastName => (placeholder.Value, user.LastName),
                    _emailAddress => (placeholder.Value, user.EmailAddress),
                    _date => (placeholder.Value, DateTimeOffset.UtcNow.ToString("dd.MM.yyyy")),
                    _companyName => (placeholder.Value, "AirBnB"),
                    _ => throw new EvaluateException("Invalid Exeption")
                };

                return new KeyValuePair<string, string>(placeholder.Value, value.Item2);
            });

            foreach (var value in placeholdersWithValues)
                values[value.Key] = value.Value;
        }
         return values;
    }

    private List<MatchCollection> GetPlaceholeders(EmailTemplate emailTemplate)
    {
        var pattern = @"\{\{([^\{\}]+)\}\}";

        var updatedSubject = Regex.Matches(emailTemplate.Subject, pattern);
        var updatedBody = Regex.Matches(emailTemplate.Body, pattern);
        
        return new List<MatchCollection>() {updatedSubject, updatedBody};
    }
}