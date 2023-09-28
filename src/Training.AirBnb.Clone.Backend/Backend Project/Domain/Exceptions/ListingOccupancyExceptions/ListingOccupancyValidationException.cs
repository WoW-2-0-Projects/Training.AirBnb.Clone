namespace Backend_Project.Domain.Exceptions.ListingOccupancyExceptions
{
    public class ListingOccupancyValidationException : Exception
    {
        public ListingOccupancyValidationException() { }
        public ListingOccupancyValidationException(string message) : base(message) { }
    }
}
