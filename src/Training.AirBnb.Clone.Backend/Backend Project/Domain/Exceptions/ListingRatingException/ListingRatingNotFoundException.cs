namespace Backend_Project.Domain.Exceptions.ListingRatingException
{
    public class ListingRatingNotFoundException : Exception
    {
        public ListingRatingNotFoundException(string message) : base (message) { }
    }
}
