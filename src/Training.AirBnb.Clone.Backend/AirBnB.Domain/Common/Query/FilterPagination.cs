namespace AirBnB.Domain.Common.Query;

/// <summary>
/// Represents a pagination filter with page size and page token for querying a collection of items.
/// </summary>
public class FilterPagination : IQueryConvertible
{
    /// <summary>
    /// Gets or sets the number of items to include on each page.
    /// </summary>
    public uint PageSize { get; set; }

    /// <summary>
    /// Gets or sets the token representing the page to retrieve in a paginated collection.
    /// </summary>
    public uint PageToken { get; set; }

    /// <summary>
    ///  Initializes a new instance of the <see cref="FilterPagination"/> class with specified page size and page token.
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageToken"></param>
    public FilterPagination(uint pageSize, uint pageToken)
    {
        PageSize = pageSize;
        PageToken = pageToken;
    }

    public FilterPagination()
    {
    }
    /// <summary>
    /// Gets the hash code for the current <see cref="FilterPagination"/> instance.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    public QueryPagination ToQueryPagination(bool asNoTracking = false) => new(PageSize, PageToken, asNoTracking);

    public virtual QuerySpecification ToQuerySpecification(bool asNoTracking = false) =>
        throw new NotSupportedException($"Filter pagination doesn't support converting to query specification");

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="FilterPagination"/> instance.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        return obj is FilterPagination filterPagination && filterPagination.GetHashCode() == GetHashCode();
    }
}