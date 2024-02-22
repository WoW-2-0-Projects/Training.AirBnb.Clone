namespace AirBnB.Domain.Common.Queries;

/// <summary>
/// Represents query options
/// </summary>
public struct QueryOptions
{
    /// <summary>
    /// Determines whether to track the query results or not
    /// </summary>
    public bool AsNoTracking { get; set; }

    /// <summary>
    /// Creates default query options instance
    /// </summary>
    public static QueryOptions Default => new();
}