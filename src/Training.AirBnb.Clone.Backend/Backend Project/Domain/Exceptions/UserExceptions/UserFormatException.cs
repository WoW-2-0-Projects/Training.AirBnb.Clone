namespace Backend_Project.Domain.Exceptions.User;

public class UserFormatException:Exception
{
    public UserFormatException(string message) : base(message) { }
}
