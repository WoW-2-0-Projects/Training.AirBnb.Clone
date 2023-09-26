namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationAlreadyExistsException : Exception
    {
        public ReservationAlreadyExistsException() { }
        public ReservationAlreadyExistsException(string message) : base(message) { }
    }
}
