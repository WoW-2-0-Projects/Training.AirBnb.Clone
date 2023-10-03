namespace Backend_Project.Domain.Exceptions.User;

public class UserNotFoundException:Exception
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string message) : base(message) { }
}
