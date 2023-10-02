namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailExceptions;

public class EmailNotFound : Exception
{
    public EmailNotFound()
    {

    }
    public EmailNotFound(string message) : base(message) { }

}
