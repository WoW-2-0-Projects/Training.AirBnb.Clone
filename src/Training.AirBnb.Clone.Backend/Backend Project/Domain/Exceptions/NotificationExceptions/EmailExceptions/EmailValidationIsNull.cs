namespace Backend_Project.Domain.Exceptions.NotificationExceptions.EmailExceptions;

public class EmailValidationIsNull : Exception
{
    public EmailValidationIsNull()
    {

    }
    public EmailValidationIsNull(string message) : base(message) { }

}
