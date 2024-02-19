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

    public static QueryOptions Default => new();
}