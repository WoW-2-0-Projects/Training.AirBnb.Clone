namespace Backend_Project.Domain.Exceptions.AddressExceptions
{
    public class AddressFormatException : Exception
    {
        public AddressFormatException() { }
        public AddressFormatException(string message)  : base(message) { }
    }
}
