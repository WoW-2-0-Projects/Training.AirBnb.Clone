namespace Backend_Project.Domain.Exceptions.AmenityCategoryException
{
    public class AmenityCategoryNotFoundException : Exception
    {
        public AmenityCategoryNotFoundException() { }
        public AmenityCategoryNotFoundException(string message) : base(message) { }
    }
}
