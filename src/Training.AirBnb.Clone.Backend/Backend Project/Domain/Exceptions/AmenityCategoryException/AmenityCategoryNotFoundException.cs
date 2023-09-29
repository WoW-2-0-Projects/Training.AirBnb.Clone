namespace Backend_Project.Domain.Exceptions.AmentyCategoryException
{
    public class AmenityCategoryNotFoundException : Exception
    {
        public AmenityCategoryNotFoundException() { }
        public AmenityCategoryNotFoundException(string message) : base(message) { }
    }
}
