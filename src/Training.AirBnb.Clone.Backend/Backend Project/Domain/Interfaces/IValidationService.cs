namespace Backend_Project.Domain.Interfaces;

public interface IValidationService
{
    ValueTask<bool> IsValidNameAsync(string name);

    bool IsValidEmailAddress(string emailAddress);

    bool IsValidPhoneNumber(string phoneNumber);
}
