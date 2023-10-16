namespace Backend_Project.Application.Validation;

public interface IValidationService
{
    bool IsValidNameAsync(string name);

    bool IsValidEmailAddress(string emailAddress);
}
