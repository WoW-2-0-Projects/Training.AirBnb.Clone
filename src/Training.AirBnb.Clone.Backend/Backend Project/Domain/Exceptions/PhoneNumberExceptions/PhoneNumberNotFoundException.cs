namespace Backend_Project.Domain.Exceptions.PhoneNumberExceptions;

public class PhoneNumberNotFoundException : Exception
{
    public PhoneNumberNotFoundException() { }

    public PhoneNumberNotFoundException(string message) : base(message) { }
}
