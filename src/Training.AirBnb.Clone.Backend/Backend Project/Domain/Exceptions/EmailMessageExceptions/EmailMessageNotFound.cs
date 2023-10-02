namespace Backend_Project.Domain.Exceptions.EmailMessageExceptions;

public class EmailMessageNotFound : Exception
{
    public EmailMessageNotFound()
    {
        
    }
    public EmailMessageNotFound(string message) : base(message)
    {
        
    }
}
