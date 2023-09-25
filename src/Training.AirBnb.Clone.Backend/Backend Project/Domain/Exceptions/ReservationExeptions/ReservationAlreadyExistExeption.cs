namespace Backend_Project.Domain.Exceptions.ReservationExeptions
{
    public class ReservationAlreadyExistExeption : Exception
    {
        public ReservationAlreadyExistExeption() { }
        public ReservationAlreadyExistExeption(string message) : base(message) { }
    }
}
