namespace Backend_Project.Domain.Exceptions.ListingExceptions;

public class AmenityAlreadyExistsException : Exception
{
    public AmenityAlreadyExistsException() { }

    public AmenityAlreadyExistsException(string message) : base(message) { }
}