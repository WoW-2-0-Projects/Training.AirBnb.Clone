namespace Backend_Project.Domain.Exceptions.ListingRatingException
{
    public class ListingRatingAlreadyExistsException : Exception
    {
        public ListingRatingAlreadyExistsException(string message) : base (message) { }
    }
}
