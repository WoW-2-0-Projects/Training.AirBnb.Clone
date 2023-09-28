
namespace Backend_Project.Domain.Exceptions.UserCredentialsExceptions;

public class UserCredentialsNotFoundException : Exception
{
    public UserCredentialsNotFoundException() { }
    public UserCredentialsNotFoundException(string? message) : base(message)
    {
    }
}
