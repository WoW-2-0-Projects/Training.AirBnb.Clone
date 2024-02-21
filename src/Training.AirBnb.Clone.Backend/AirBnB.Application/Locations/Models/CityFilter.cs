using AirBnB.Domain.Common.Query;

namespace AirBnB.Application.Locations.Models;

/// <summary>
/// Represents a filter for querying cities, including pagination settings.
/// </summary>
public class CityFilter : FilterPagination
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CityFilter"/> class with default pagination settings.
    /// </summary>
    public CityFilter()
    {
        PageSize = 50;
        PageToken = 1;
    }

    /// <summary>
    /// Computes a hash code for the current <see cref="CityFilter"/> object.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current <see cref="CityFilter"/> object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => obj is CityFilter cityFilter
           && cityFilter.GetHashCode() == GetHashCode();
}
