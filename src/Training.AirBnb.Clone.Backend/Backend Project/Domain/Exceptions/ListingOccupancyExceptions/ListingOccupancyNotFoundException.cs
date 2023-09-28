namespace Backend_Project.Domain.Exceptions.ListingOccupancyExceptions
{
    public class ListingOccupancyNotFoundException : Exception
    {
        public ListingOccupancyNotFoundException() { }
        public ListingOccupancyNotFoundException(string message) : base(message) { }
    }
}
