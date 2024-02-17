using AirBnB.Domain.Common.Query;

namespace AirBnB.Application.Locations.Models;

/// <summary>
/// Represents a filter for querying countries, including pagination settings.
/// </summary>
public class CountryFilter : FilterPagination
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CountryFilter"/> class with default pagination settings.
    /// </summary>
    public CountryFilter()
    {
        PageSize = int.MaxValue;
        PageToken = 1;
    }

    /// <summary>
    /// Computes a hash code for the current <see cref="CountryFilter"/> object.
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
    /// Determines whether the specified object is equal to the current <see cref="CountryFilter"/> object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => obj is CountryFilter countryFilter
           && countryFilter.GetHashCode() == GetHashCode();
}
