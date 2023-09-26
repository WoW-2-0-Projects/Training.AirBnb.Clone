namespace Backend_Project.Domain.Exceptions.ListingReviewException
{
    public class ListingReviewNotFoundException : Exception
    {
        public ListingReviewNotFoundException(string message) : base(message) { }
    }
}
