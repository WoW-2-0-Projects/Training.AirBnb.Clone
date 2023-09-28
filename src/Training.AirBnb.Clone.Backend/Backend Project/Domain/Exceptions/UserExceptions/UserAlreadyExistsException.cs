namespace Backend_Project.Domain.Exceptions.User;

public class UserAlreadyExistsException:Exception
{
    public UserAlreadyExistsException() { } 
    public UserAlreadyExistsException(string message) : base(message){}
}
