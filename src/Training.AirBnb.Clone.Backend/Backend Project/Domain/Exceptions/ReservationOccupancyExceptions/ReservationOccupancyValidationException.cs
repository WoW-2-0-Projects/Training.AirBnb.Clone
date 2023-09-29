namespace Backend_Project.Domain.Exceptions.ListingOccupancyExceptions
{
    public class ReservationOccupancyValidationException : Exception
    {
        public ReservationOccupancyValidationException() { }
        public ReservationOccupancyValidationException(string message) : base(message) { }
    }
}
