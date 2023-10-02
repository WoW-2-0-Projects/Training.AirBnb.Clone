namespace Backend_Project.Domain.Exceptions.AmenityCategoryException
{
    public class AmenityCategoryAlreadyExistsException : Exception
    {
        public AmenityCategoryAlreadyExistsException() { }
        public AmenityCategoryAlreadyExistsException(string message) : base(message) { }
    }
}
