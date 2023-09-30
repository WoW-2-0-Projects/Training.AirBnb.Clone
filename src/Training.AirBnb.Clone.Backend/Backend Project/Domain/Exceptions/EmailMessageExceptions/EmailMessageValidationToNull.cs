namespace Backend_Project.Domain.Exceptions.EmailMessageExceptions;

public class EmailMessageValidationToNull : Exception
{
    public EmailMessageValidationToNull()
    {
        
    }
    public EmailMessageValidationToNull(string message) : base(message)
    {
        
    }
}
