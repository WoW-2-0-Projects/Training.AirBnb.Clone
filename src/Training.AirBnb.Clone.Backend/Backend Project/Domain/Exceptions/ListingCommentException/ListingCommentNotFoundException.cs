namespace Backend_Project.Domain.Exceptions.ListingReviewException
{
    public class ListingCommentNotFoundException : Exception
    {
        public ListingCommentNotFoundException(string message) : base(message) { }
    }
}
