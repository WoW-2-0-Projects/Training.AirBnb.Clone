
namespace Backend_Project.Domain.Exceptions.UserCredentialsExceptions;

public class UserCredentialsAlreadyExistsException : Exception
{
    public UserCredentialsAlreadyExistsException()
    {
    }

    public UserCredentialsAlreadyExistsException(string? message) : base(message)
    {
    }
}
