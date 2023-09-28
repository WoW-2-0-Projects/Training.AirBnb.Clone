
namespace Backend_Project.Domain.Exceptions.UserCredentialsExceptions;

public class NotValidUserCredentialsException : Exception
{
    public NotValidUserCredentialsException() : base() { }
    public NotValidUserCredentialsException(string message) : base(message)
    {
    }
}
