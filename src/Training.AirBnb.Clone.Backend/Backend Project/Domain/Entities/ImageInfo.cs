using Backend_Project.Domain.Common;
using Backend_Project.Domain.Enums;

namespace Backend_Project.Domain.Entities;

public class ImageInfo : SoftDeletedEntity
{
    public string FilePath { get; set; } = default!;

    public string Extension { get; set; } = default!;

    public long Size { get; set; } 

    public Guid UserId { get; set; }

    public Guid? ListingId { get; set; }

    public ImageType Type { get; set; }
}