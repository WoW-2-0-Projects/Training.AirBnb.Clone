
namespace Backend_Project.Domain.Exceptions.UserCredentialsExceptions;

public class UserCredentailsAlreadyExistsException:Exception
{
    public UserCredentailsAlreadyExistsException()
    {
    }
    public UserCredentailsAlreadyExistsException(string? message) : base(message)
    {
    }
}
