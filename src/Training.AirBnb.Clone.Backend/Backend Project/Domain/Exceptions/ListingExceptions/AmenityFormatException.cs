namespace Backend_Project.Domain.Exceptions.ListingExceptions;

public class AmenityFormatException : Exception
{
    public AmenityFormatException() { }

    public AmenityFormatException(string message) : base(message) { }
}