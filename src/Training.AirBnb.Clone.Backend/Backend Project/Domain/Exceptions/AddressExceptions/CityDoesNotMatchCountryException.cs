namespace Backend_Project.Domain.Exceptions.AddressExceptions
{
    public class CityDoesNotMatchCountryException : Exception
    {
        public CityDoesNotMatchCountryException() { }
        public CityDoesNotMatchCountryException(string message) : base(message) { }
    }
}
