namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    internal class ReservationCreateExeption : Exception
    {
        public ReservationCreateExeption() { }
        public ReservationCreateExeption(string message) : base(message) { }
    }
}
