namespace Backend_Project.Application.Validation.Services;

public interface IValidationService
{
    bool IsValidNameAsync(string name);

    bool IsValidEmailAddress(string emailAddress);
}
