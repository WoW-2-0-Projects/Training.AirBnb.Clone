namespace Backend_Project.Application.Interfaces;

public interface IValidationService
{
    bool IsValidNameAsync(string name);

    bool IsValidEmailAddress(string emailAddress);
}
