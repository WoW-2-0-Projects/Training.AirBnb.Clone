namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailTemplateExceptions;

public class EmailTemplateValidationToNull : Exception
{
    public EmailTemplateValidationToNull()
    {

    }
    public EmailTemplateValidationToNull(string message) : base(message)
    {

    }
}
