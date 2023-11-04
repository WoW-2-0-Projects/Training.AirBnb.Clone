using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities;

public class ListingImage : SoftDeletedEntity
{
    public string FilePath { get; set; } = default!;

    public string Extension { get; set; } = default!;

    public long Size { get; set; } 

    public Guid ListingId { get; set; }
}