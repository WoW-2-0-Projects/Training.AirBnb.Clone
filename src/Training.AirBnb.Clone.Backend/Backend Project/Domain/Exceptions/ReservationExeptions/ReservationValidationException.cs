namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationValidationException : Exception
    {
        public ReservationValidationException() { }
        public ReservationValidationException(string message) : base(message) { }
    }
}
