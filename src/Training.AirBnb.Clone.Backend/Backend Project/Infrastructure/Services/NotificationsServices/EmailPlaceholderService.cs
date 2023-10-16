using Backend_Project.Application.Interfaces;
using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Exceptions.EntityExceptions;
using System.Data;
using System.Text;

namespace Backend_Project.Infrastructure.Services.NotificationsServices;

public class EmailPlaceholderService : IEmailPlaceholderService
{
    private readonly IEntityBaseService<User> _userService;
    private const string _fullName = "{{FullName}}";
    private const string _firstName = "{{FirstName}}";
    private const string _lastName = "{{LastName}}";
    private const string _emailAddress = "{{EmailAddress}}";
    private const string _date = "{{Date}}";
    private const string _companyName = "{{CompanyName}}";
   
    public EmailPlaceholderService(IEntityBaseService<User> user)
    {
        _userService = user;
    }
    public async ValueTask<Dictionary<string, string>> GetTemplateValues(Guid userId, EmailTemplate emailTemplate)
    {
        var placeholders = GetPlaceholeders(emailTemplate.Subject,emailTemplate.Body);
        
        var user = await _userService.GetByIdAsync(userId) ?? throw new EntityNotFoundException<User>();

        var result = placeholders.Select(placeholder =>
        {
            var value = placeholder switch
            {
                _fullName => $"{user.FirstName} {user.LastName}",
                _firstName => user.FirstName,
                _lastName => user.LastName,
                _emailAddress => user.EmailAddress,
                _date => DateTime.UtcNow.ToString("dd.MM.yyyy"),
                _companyName => "AirBnB",
                _ => throw new EvaluateException("Invalid Exeption")
            };
            return new KeyValuePair<string, string>(placeholder, value);
        });

        var values = new Dictionary<string, string>(result);

        return values;
    }

    private IEnumerable<string> GetPlaceholeders(string subject, string body)
    {
        var plaseholder = new StringBuilder();
        var isStartedToGether = false;

        for (var indexA = 0; indexA < body.Length; indexA++)
        {
            if (body[indexA] == '{')
            {
                indexA++;
                plaseholder = new StringBuilder();
                plaseholder.Append("{{");
                isStartedToGether = true;
            }
            else if (body[indexA] == '}')
            {
                indexA++;
                plaseholder.Append("}}");
                isStartedToGether = false;
                yield return plaseholder.ToString();
            }
            else if (isStartedToGether)
            {
                plaseholder.Append(body[indexA]);
            }
        }
        for (var indexB = 0; indexB < subject.Length; indexB++)
        {
            if (subject[indexB] == '{')
            {
                indexB++;
                plaseholder = new StringBuilder();
                plaseholder.Append("{{");
                isStartedToGether = true;
            }
            else if (subject[indexB] == '}')
            {
                indexB++;
                plaseholder.Append("}}");
                isStartedToGether = false;
                yield return plaseholder.ToString();
            }
            else if (isStartedToGether)
            {
                plaseholder.Append(subject[indexB]);
            }
        }
    }
}