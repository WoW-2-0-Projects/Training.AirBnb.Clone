namespace Backend_Project.Domain.Exceptions.ListingOccupancyExceptions
{
    public class ReservationOccupancyNotFoundException : Exception
    {
        public ReservationOccupancyNotFoundException() { }
        public ReservationOccupancyNotFoundException(string message) : base(message) { }
    }
}
