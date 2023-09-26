namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationNotFound : Exception
    {
        public ReservationNotFound() { }
        public ReservationNotFound(string message) : base(message) { }
    }
}
