namespace Backend_Project.Domain.Exceptions.ListingRatingException
{
    public class ListingRatingAlreadyExistsException : Exception
    {
        public ListingRatingAlreadyExistsException() { }
        public ListingRatingAlreadyExistsException(string message) : base (message) { }
    }
}
