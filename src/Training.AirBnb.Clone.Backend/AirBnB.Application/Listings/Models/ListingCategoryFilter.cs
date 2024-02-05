using AirBnB.Domain.Common.Query;

namespace AirBnB.Application.Listings.Models;

/// <summary>
/// Represents a listing category filter
/// </summary>
public class ListingCategoryFilter : FilterPagination
{
    public ListingCategoryFilter()
    {
        PageSize = int.MaxValue;
        PageToken = 1;
    }
    
    /// <summary>
    /// Overrides base GetHashCode method
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    /// <summary>
    /// Overrides base Equals method
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj) => 
        obj is ListingCategoryFilter listingCategoryFilter 
            && listingCategoryFilter.GetHashCode() == GetHashCode();
}