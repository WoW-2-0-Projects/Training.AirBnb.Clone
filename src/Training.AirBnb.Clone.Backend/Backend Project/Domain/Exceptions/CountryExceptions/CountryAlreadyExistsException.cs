namespace Backend_Project.Domain.Exceptions.CountryExceptions
{
    public class CountryAlreadyExistsException : Exception
    {
        public CountryAlreadyExistsException() { }
        public CountryAlreadyExistsException(string message) : base(message) { }
    }
}
