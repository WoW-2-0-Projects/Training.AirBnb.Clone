namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationCreateExeption : Exception
    {
        public ReservationCreateExeption() { }
        public ReservationCreateExeption(string message) : base(message) { }
    }
}
