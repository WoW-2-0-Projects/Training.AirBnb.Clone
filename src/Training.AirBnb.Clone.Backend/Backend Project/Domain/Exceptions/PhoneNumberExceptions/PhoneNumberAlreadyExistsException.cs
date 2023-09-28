namespace Backend_Project.Domain.Exceptions.PhoneNumberExceptions;

public class PhoneNumberAlreadyExistsException : Exception
{
    public PhoneNumberAlreadyExistsException() { }

    public PhoneNumberAlreadyExistsException (string message) : base(message) { }
}
