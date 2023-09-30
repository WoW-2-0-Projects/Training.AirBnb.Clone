namespace Backend_Project.Domain.Exceptions.EmailMessageExceptions;

public class EmailMessageAlreadyExists : Exception
{
    public EmailMessageAlreadyExists()
    {
        
    }

    public EmailMessageAlreadyExists(string message) : base(message) { }
  
}
