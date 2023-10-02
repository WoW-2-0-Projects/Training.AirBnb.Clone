namespace Backend_Project.Domain.Exceptions.ListingRatingException
{
    public class ListingRatingFormatException : Exception
    {
        public ListingRatingFormatException() { }
        public ListingRatingFormatException(string message) : base(message) { }
    }
}
