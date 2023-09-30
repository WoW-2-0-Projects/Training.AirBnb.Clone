namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailTemplateExceptions;

public class EmailTemplateAlreadyExists : Exception
{
    public EmailTemplateAlreadyExists()
    {

    }
    public EmailTemplateAlreadyExists(string message) : base(message)
    {

    }
}
