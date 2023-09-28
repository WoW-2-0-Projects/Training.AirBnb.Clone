namespace Backend_Project.Domain.Exceptions.PhoneNumberExceptions;

public class PhoneNumberFormatException : Exception
{
    public PhoneNumberFormatException() { }

    public PhoneNumberFormatException(string message) : base(message) { }
}
