namespace AirBnB.Domain.Common.Query;

public class FilterPagination
{
    public uint PageSize { get; set; }
    public uint PageToken { get; set; }

    public FilterPagination()
    {
    }
    
    public FilterPagination(uint pageSize, uint pageToken)
    {
        PageSize = pageSize;
        PageToken = pageToken;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        
        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is FilterPagination filterPagination && filterPagination.GetHashCode() == GetHashCode();
    }
}