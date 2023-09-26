namespace Backend_Project.Domain.Exceptions.CountryExceptions
{
    public class CountryFormatException : Exception
    {
        public CountryFormatException() { }
        public CountryFormatException(string message) : base(message) { }
    }
}
