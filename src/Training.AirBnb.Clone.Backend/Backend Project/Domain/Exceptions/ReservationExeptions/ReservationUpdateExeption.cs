namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationUpdateExeption : Exception
    {
        public ReservationUpdateExeption() { }
        public ReservationUpdateExeption(string message) : base(message) { }
    }
}
