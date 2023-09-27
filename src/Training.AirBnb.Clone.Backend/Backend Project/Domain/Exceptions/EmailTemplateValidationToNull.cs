namespace Backend_Project.Domain.Exceptions;

public class EmailTemplateValidationToNull : Exception
{
    public EmailTemplateValidationToNull()
    {
        
    }
    public EmailTemplateValidationToNull(string message) : base(message)
    {
        
    }
}
