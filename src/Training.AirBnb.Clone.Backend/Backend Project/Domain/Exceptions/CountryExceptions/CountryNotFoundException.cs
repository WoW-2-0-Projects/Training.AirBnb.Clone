namespace Backend_Project.Domain.Exceptions.CountryExceptions
{
    public class CountryNotFoundException : Exception
    {
        public CountryNotFoundException() { }
        public CountryNotFoundException(string message) : base(message) { }
    }
}
