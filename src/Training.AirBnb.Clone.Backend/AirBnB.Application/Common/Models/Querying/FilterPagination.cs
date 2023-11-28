namespace AirBnB.Application.Common.Models.Querying;

/// <summary>
/// Created pagination for templates
/// </summary>
public class FilterPagination
{
    public uint PageSize { get; set; } = 10;

    public uint PageToken { get; set; } = 1;
}