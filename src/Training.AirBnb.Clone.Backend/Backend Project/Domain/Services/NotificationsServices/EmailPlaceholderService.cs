using Backend_Project.Domain.Entities;
using Backend_Project.Domain.Interfaces;
using System.Data;
using System.Text;
namespace Backend_Project.Domain.Services.NotificationService;

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
    public async ValueTask<Dictionary<string, string>> GEtTemplateValues(Guid userId, EmailTemplate emailTemplate)
    {
        var placeholders = GetPlaceholeders(emailTemplate.Body);
        var user = await _userService.GetByIdAsync(userId) ?? throw new ArgumentException();

        var result = placeholders.Select(placeholder =>
        {
            var value = placeholder switch
            {
                _fullName => string.Join(_firstName, " ", _lastName),
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

    private IEnumerable<string> GetPlaceholeders(string body)
    {
        var plaseholder = new StringBuilder();
        var isStartedToGether = false;

        for (var index = 0; index < body.Length; index++)
        {
            if (body[index] == '{')
            {
                index++;
                plaseholder = new StringBuilder();
                plaseholder.Append("{{");
                isStartedToGether = true;
            }
            else if (body[index] == '}')
            {
                index++;
                plaseholder.Append("}}");
                isStartedToGether = false;
                yield return plaseholder.ToString();
            }
            else if (isStartedToGether)
            {
                plaseholder.Append(body[index]);
            }
        }
    }
}
