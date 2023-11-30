namespace AirBnB.Application.Common.Models.Querying;

/// <summary>
/// Created pagination for templates
/// </summary>
public class FilterPagination
{
    /// <summary>
    /// page size for pagination
    /// </summary>
    public uint PageSize { get; set; } = 10;

    /// <summary>
    /// page Token for pagination
    /// </summary>
    public uint PageToken { get; set; } = 1;
}