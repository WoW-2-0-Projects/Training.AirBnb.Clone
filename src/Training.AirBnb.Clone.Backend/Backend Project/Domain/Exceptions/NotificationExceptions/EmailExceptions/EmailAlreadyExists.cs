namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailExceptions;

public class EmailAlreadyExists : Exception
{
    public EmailAlreadyExists()
    {

    }
    public EmailAlreadyExists(string message) : base(message) { }

}
