namespace Backend_Project.Domain.Exceptions;

public class EmailTemplateAlreadyExists : Exception
{
    public EmailTemplateAlreadyExists()
    {
        
    }
    public EmailTemplateAlreadyExists(string message) :base(message)
    {
        
    }
}
