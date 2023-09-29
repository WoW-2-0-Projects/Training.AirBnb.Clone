namespace Backend_Project.Domain.Exceptions.ListingExceptions;

public class AmenityNotFoundException : Exception
{
    public AmenityNotFoundException() { }

    public AmenityNotFoundException(string message) : base(message) { }
}