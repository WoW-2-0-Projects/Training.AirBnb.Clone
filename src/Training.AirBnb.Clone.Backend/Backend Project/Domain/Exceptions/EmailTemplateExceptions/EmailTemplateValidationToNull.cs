namespace Backend_Project.Domain.Exceptions.EmailTemplateExceptions;

public class EmailTemplateValidationToNull : Exception
{
    public EmailTemplateValidationToNull()
    {

    }
    public EmailTemplateValidationToNull(string message) : base(message)
    {

    }
}
