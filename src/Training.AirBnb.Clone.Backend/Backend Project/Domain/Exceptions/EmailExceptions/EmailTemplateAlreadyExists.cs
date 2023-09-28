namespace Backend_Project.Domain.Exceptions.EmailExceptions;

public class EmailTemplateAlreadyExists : Exception
{
    public EmailTemplateAlreadyExists()
    {

    }
    public EmailTemplateAlreadyExists(string message) : base(message)
    {

    }
}
